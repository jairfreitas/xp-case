using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XpCase.Application.DTOs.Order;
using XpCase.Application.Interfaces;

namespace XpCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]
[SwaggerTag("Endpoints para gerenciar Ordens")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todas as Ordens", Description = "Retorna uma lista de todos as Ordens.")]
    [SwaggerResponse(200, "Lista de Ordens obtida com sucesso", typeof(IEnumerable<OrderDto>))]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var orders = await orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter uma Ordem",
        Description = "Retorna os detalhes de uma Ordem específico pelo ID.")]
    [SwaggerResponse(200, "Ordem obtida com sucesso", typeof(OrderDto))]
    [SwaggerResponse(404, "Ordem não encontrado")]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);

        return Ok(order);
    }

    [HttpPost("sell")]
    [SwaggerOperation(Summary = "Criar uma Ordem de venda", Description = "Cria uma nova Ordem de venda.")]
    [SwaggerResponse(201, "Ordem criada com sucesso.")]
    [SwaggerResponse(400, "Dados inválidos.")]
    [SwaggerResponse(422, "Quantidade insuficiente de ativos na carteira.")]
    [SwaggerResponse(403, "Saldo insuficiente para comprar os ativos.")]
    public async Task<IActionResult> SellOrder([FromBody] CreateOrderDto createOrderDto)
    {
        try
        {
            var orderId = await orderService.SellAsync(createOrderDto);

            return CreatedAtAction(nameof(GetOrder), new { id = orderId },
                new { message = "Ordem criada com sucesso" });
        }
        catch (InvalidOperationException ex)
        {
            switch (ex.Message)
            {
                case "Quantidade insuficiente de ativos na carteira.":
                    return UnprocessableEntity(new { message = ex.Message });
                default:
                    throw;
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("buy")]
    [SwaggerOperation(Summary = "Cria uma Ordem de compra", Description = "Cria uma nova Ordem de compra.")]
    [SwaggerResponse(201, "Ordem criada com sucesso.")]
    [SwaggerResponse(400, "Dados inválidos.")]
    [SwaggerResponse(422, "Quantidade insuficiente de ativos na carteira.")]
    [SwaggerResponse(403, "Saldo insuficiente para comprar os ativos.")]
    public async Task<IActionResult> BuyOrder([FromBody] CreateOrderDto createOrderDto)
    {
        try
        {
            var orderId = await orderService.BuyAsync(createOrderDto);

            return CreatedAtAction(nameof(GetOrder), new { id = orderId },
                new { message = "Ordem criada com sucesso" });
        }
        catch (InvalidOperationException ex)
        {
            switch (ex.Message)
            {
                case "Saldo insuficiente para comprar os ativos.":
                    return StatusCode(403, new { message = ex.Message });
                default:
                    throw;
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("execute{id}")]
    [SwaggerOperation(Summary = "Executa uma Ordem",
        Description = "Executa uma nova Ordem e define para (Approved/Rejected/Pending/Partial).")]
    [SwaggerResponse(204, "Ordem executada com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    [SwaggerResponse(422, "Esse Customer não tem recursos para executar essa Ordem.")]
    [SwaggerResponse(403, "Ordem não encontrada com status Pending ou Partial.")]
    public async Task<IActionResult> PutOrder(Guid id)
    {
        try
        {
            var order = await orderService.GetPendingOrPartialOrderAsync(id);

            await orderService.MatchingEngineAsync(order);

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId },
                new { message = "Ordem processada com sucesso" });
        }
        catch (InvalidOperationException ex)
        {
            switch (ex.Message)
            {
                case "Ordem não encontrada com status Pending ou Partial.":
                    return StatusCode(403, new { message = ex.Message });
                case "Esse Customer não tem recursos para executar essa Ordem.":
                    return StatusCode(422, new { message = ex.Message });
                default:
                    throw;
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPut("execute-batch")]
    [SwaggerOperation(Summary = "Executa um lote de  Ordem",
        Description = "Executa um lote de Ordem e define para (Approved/Rejected/Pending/Partial).")]
    [SwaggerResponse(204, "Ordem executada com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    [SwaggerResponse(422, "Esse Customer não tem recursos para executar essa Ordem.")]
    [SwaggerResponse(403, "Ordem não encontrada com status Pending ou Partial.")]
    public async Task<IActionResult> PutOrder()
    {
        try
        {
            var ordersId = await orderService.GetAllIdPendingOrPartialOrderAsync();

            var orderOperationException = new List<OrderDto>();

            var order = new OrderDto();

            foreach (var id in ordersId)
                try
                {
                    order = await orderService.GetPendingOrPartialOrderAsync(id);

                    if (order.Status == "Partial" || order.Status == "Pending")
                        await orderService.MatchingEngineAsync(order);
                }
                catch (InvalidOperationException ex)
                {
                    switch (ex.Message)
                    {
                        case "Esse Customer não tem recursos para executar essa Ordem.":
                            orderOperationException.Add(order);
                            continue;
                    }
                }

            if (orderOperationException.Any())
                return Ok(new { Message = "Algumas ordens foram rejeitadas!", orderOperationException });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            switch (ex.Message)
            {
                case "Ordem não encontrada com status Pending ou Partial.":
                    return StatusCode(403, new { message = ex.Message });
                case "Esse Customer não tem recursos para executar essa Ordem.":
                    return StatusCode(422, new { message = ex.Message });
                default:
                    throw;
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Excluir uma Ordem", Description = "Exclui uma Ordem existente pelo ID.")]
    [SwaggerResponse(204, "Ordem excluído com sucesso")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await orderService.DeleteAsync(id);
        return NoContent();
    }
}