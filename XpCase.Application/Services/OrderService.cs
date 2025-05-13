using AutoMapper;
using XpCase.Application.DTOs.Order;
using XpCase.Application.Interfaces;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;

namespace XpCase.Application.Services;

public class OrderService : IOrderService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IAssetService _assetService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWalletRepository _walletRepository;

    public OrderService(IOrderRepository orderRepository, IWalletRepository walletRepository,
        ICustomerRepository customerRepository, IAssetRepository assetRepository, IAssetService assetService,
        IUnitOfWork unitOfWork, ITransactionRepository transactionRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _walletRepository = walletRepository;
        _customerRepository = customerRepository;
        _assetRepository = assetRepository;
        _assetService = assetService;
        _unitOfWork = unitOfWork;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.FindAsync(o => o.OrderId == id);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _orderRepository.UpdateAsync(order);
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await GetByIdAsync(id);

        if (order.Status != "Reject")
            throw new InvalidOperationException(
                $"A Ordem não pode ser  excluída porque está com status de {order.Status}.");

        await _orderRepository.DeleteAsync(id);
    }

    public async Task<Guid> SellAsync(CreateOrderDto createOrderDto)
    {
        var hasAssetsToOrder = await _walletRepository.HasAssetsToOrder(createOrderDto.CustomerId,
            createOrderDto.AssetId, createOrderDto.Quantity);

        if (!hasAssetsToOrder)
            throw new InvalidOperationException("Quantidade insuficiente de ativos na carteira.");

        var order = _mapper.Map<Order>(createOrderDto);

        // Definir o status como Pending
        order.Status = "Pending";
        order.IsBuyOrder = false;

        await _orderRepository.AddAsync(order);

        return order.OrderId;
    }

    public async Task<Guid> BuyAsync(CreateOrderDto createOrderDto)
    {
        var hasAmountToOrder = await _customerRepository.HasAmountToOrder(createOrderDto.CustomerId,
            createOrderDto.Quantity, createOrderDto.Price);

        if (!hasAmountToOrder)
            throw new InvalidOperationException("Saldo insuficiente para comprar os ativos.");

        var order = _mapper.Map<Order>(createOrderDto);

        // Definir o status como Pending
        order.Status = "Pending";
        order.IsBuyOrder = true;

        await _orderRepository.AddAsync(order);

        return order.OrderId;
    }

    public async Task<OrderDto> GetPendingOrPartialOrderAsync(Guid id)
    {
        var order = await _orderRepository.FindAsync(
            o => o.OrderId == id && (o.Status == "Pending" || o.Status == "Partial"), true);

        if (order == null)
            throw new InvalidOperationException("Ordem não encontrada com status Pending ou Partial.");

        return _mapper.Map<OrderDto>(order);
    }

    public async Task MatchingEngineAsync(OrderDto newOrder)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var hasResourcesToOrder = await HasResourcesToOrderAsync(newOrder);

            if (!hasResourcesToOrder)
                throw new InvalidOperationException("Esse Customer não tem recursos para executar essa Ordem.");

            //OBTENDO AS ORDENS OPOSTAS
            var oppositeOrders = await _orderRepository.FindAllAsync(o =>
                o.AssetId == newOrder.AssetId && o.CustomerId != newOrder.CustomerId &&
                (o.Status == "Pending" || o.Status == "Partial") &&
                o.IsBuyOrder != newOrder.IsBuyOrder);


            //SE FOR COMPRADOR VERIFICO ANTES SE HÁ ATIVOS DISPONÍVEIS DIRETAMENTE DA EMPRESA (IPO)
            if (newOrder.IsBuyOrder)
            {
                var asset = await _assetService.GetByIdAndPriceQuantityAndValidDateAsync(newOrder.AssetId,
                    newOrder.Price, newOrder.Quantity);

                //SE HOUVER ATIVO EFETIVO A COMPRA
                if (asset != null)
                {
                    var quantity = newOrder.Quantity;

                    await ExecuteOrderAsync(newOrder, 0);
                    asset.Quantity -= quantity;
                    await _assetService.UpdateAsync(asset);

                    //ZERO A LISTA DE OPOSITORES PARA NÃO SER PROCESSADA ABAIXO
                    oppositeOrders = new List<Order>();
                }
            }

            //PROCESSANDO AS ORDENS OPOSTAS QUANDO NÃO HÁ ATIVOS DISPONÍVEIS DIRETAMENTE DA EMPRESA
            foreach (var oppositeOrder in oppositeOrders.Where(o => o.Status is "Pending" or "Partial"))
            {
                var result = await ExecuteOrderAsync(newOrder, _mapper.Map<OrderDto>(oppositeOrder));

                if (result)
                    break;
            }

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (InvalidOperationException e)
        {
            //var order = await GetByIdAsync(newOrder.OrderId);
            newOrder.Status = "Rejected";
            await _orderRepository.UpdateAsync(_mapper.Map<Order>(newOrder));
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> HasResourcesToOrderAsync(OrderDto orderDto)
    {
        switch (orderDto.IsBuyOrder)
        {
            case true:
            {
                var hasAmountToOrder =
                    await _customerRepository.HasAmountToOrder(orderDto.CustomerId, orderDto.Quantity, orderDto.Price);

                return hasAmountToOrder;
            }
            case false:
            {
                var hasAssetsToOrder =
                    await _walletRepository.HasAssetsToOrder(orderDto.CustomerId, orderDto.AssetId, orderDto.Quantity);

                return hasAssetsToOrder;
            }
        }
    }

    public async Task ExecuteOrderAsync(OrderDto orderDto, int quantityPending)
    {
        var amount = orderDto.Quantity * orderDto.Price;
        var customer = await _customerRepository.GetByIdAsync(orderDto.CustomerId);
        var wallet = await _walletRepository.GetByCustomerIdAndAssetIdAsync(orderDto.CustomerId, orderDto.AssetId);

        if (customer == null)
            throw new Exception("Customer não encontrado.");

        var transaction = new Transaction { CustomerId = orderDto.CustomerId, Name = "Stock" };

        if (orderDto.IsBuyOrder)
        {
            customer.Amount -= amount;
            await _customerRepository.UpdateAsync(customer);

            if (wallet == null)
            {
                wallet = new Wallet { CustomerId = orderDto.CustomerId, AssetId = orderDto.AssetId };
                wallet.Quantity += orderDto.Quantity;
                await _walletRepository.AddAsync(wallet);
            }
            else
            {
                wallet.Quantity += orderDto.Quantity;
                await _walletRepository.UpdateAsync(wallet);
            }

            transaction.Price = -amount;
            transaction.Type = "Buy";
        }
        else
        {
            if (wallet == null)
                throw new Exception("Wallet não encontrada.");

            customer.Amount += amount;
            await _customerRepository.UpdateAsync(customer);

            wallet.Quantity -= orderDto.Quantity;

            if (wallet.Quantity == 0)
                await _walletRepository.DeleteAsync(wallet.WalletId);
            else
                await _walletRepository.UpdateAsync(wallet);

            transaction.Price = amount;
            transaction.Type = "Sell";
        }

        if (quantityPending >= 0)
            orderDto.Quantity = quantityPending;

        if (orderDto.Quantity == 0)
            orderDto.Status = "Approved";

        await _orderRepository.UpdateAsync(_mapper.Map<Order>(orderDto));

        await _transactionRepository.AddAsync(transaction);
    }

    public async Task<bool> ExecuteOrderAsync(OrderDto newOrder, OrderDto oppositeOrder)
    {
        if ((newOrder.IsBuyOrder && oppositeOrder.Price <= newOrder.Price) ||
            (!newOrder.IsBuyOrder && oppositeOrder.Price >= newOrder.Price))
        {
            oppositeOrder.Status = "Partial";
            newOrder.Status = "Partial";

            var newOrderQuantity = newOrder.Quantity;
            var oppositeOrderQuantity = oppositeOrder.Quantity;

            // Match found, process the order
            var matchedQuantity = Math.Min(newOrder.Quantity, oppositeOrder.Quantity);
            newOrder.Quantity -= matchedQuantity;
            oppositeOrder.Quantity -= matchedQuantity;

            if (newOrder.IsBuyOrder)
            {
                if (oppositeOrder.Price < newOrder.Price) newOrder.Price = oppositeOrder.Price;
            }
            else
            {
                if (oppositeOrder.Price > newOrder.Price) newOrder.Price = oppositeOrder.Price;
            }

            if (oppositeOrder.Quantity == 0)
            {
                oppositeOrder.Status = "Approved";
                oppositeOrder.Quantity = oppositeOrderQuantity;
                await ExecuteOrderAsync(oppositeOrder, 0);
            }
            else
            {
                var quantityPending = oppositeOrder.Quantity;
                oppositeOrder.Quantity = matchedQuantity;
                await ExecuteOrderAsync(oppositeOrder, quantityPending);
            }

            if (newOrder.Quantity == 0)
            {
                newOrder.Status = "Approved";
                newOrder.Quantity = newOrderQuantity;
                await ExecuteOrderAsync(newOrder, 0);
                return true;
            }

            var quantityNewOrderPending = newOrder.Quantity;
            newOrder.Quantity = matchedQuantity;
            await ExecuteOrderAsync(newOrder, quantityNewOrderPending);
        }

        return false;
    }

    public async Task<IEnumerable<OrderDto>> GetAllPendingOrPartialOrderAsync()
    {
        var orders = await _orderRepository.FindAllAsync(o => o.Status == "Pending" || o.Status == "Partial");

        if (!orders.Any())
            throw new InvalidOperationException("Não há Ordens encontradas com status Pending ou Partial.");


        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<IEnumerable<Guid>> GetAllIdPendingOrPartialOrderAsync()
    {
        var orders = await _orderRepository.FindAllAsync(o => o.Status == "Pending" || o.Status == "Partial");

        if (!orders.Any())
            throw new InvalidOperationException("Não há Ordens encontradas com status Pending ou Partial.");


        return orders.Select(s => s.OrderId);
    }
}