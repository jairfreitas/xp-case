using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpCase.Application.DTOs.Order;

namespace XpCase.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(Guid id);
        Task UpdateAsync(OrderDto orderDto);
        Task DeleteAsync(Guid id);
        Task<Guid> SellAsync(CreateOrderDto orderDto);
        Task<Guid> BuyAsync(CreateOrderDto orderDto);
        Task<OrderDto> GetPendingOrPartialOrderAsync(Guid id);
        Task MatchingEngineAsync(OrderDto orderDto);
        Task<bool> HasResourcesToOrderAsync(OrderDto orderDto);
        Task ExecuteOrderAsync(OrderDto orderDto, int quantityPending);
        Task<bool> ExecuteOrderAsync(OrderDto newOrder, OrderDto oppositeOrder);
        Task<IEnumerable<OrderDto>> GetAllPendingOrPartialOrderAsync();
        Task<IEnumerable<Guid>> GetAllIdPendingOrPartialOrderAsync();
    }
}
