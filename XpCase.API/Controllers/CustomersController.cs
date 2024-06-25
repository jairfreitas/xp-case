using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XpCase.Application.DTOs.Customer;
using XpCase.Application.DTOs.Order;
using XpCase.Application.Interfaces;

namespace XpCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Obter um cliente",
        Description = "Retorna os detalhes de um cliente específico pelo ID.")]
    [SwaggerResponse(200, "cliente obtido com sucesso", typeof(OrderDto))]
    [SwaggerResponse(404, "cliente não encontrado")]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        var customers = await customerService.GetAllAsync();
        return Ok(customers);
    }


    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza um cliente",
        Description = "Atualiza os dados de um cliente existente pelo ID..")]
    [SwaggerResponse(204, "cliente atualizado com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    public async Task<IActionResult> PutCustomer(Guid id, CustomerDto customerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (id != customerDto.CustomerId)
            return BadRequest(new { message = "O ID do cliente fornecido não corresponde ao ID na URL." });

        var customerExists = await customerService.CustomerExistsAsync(customerDto.Email, customerDto.CustomerId);

        if (customerExists) return Conflict(new { message = "Um cliente com este email já existe." });

        await customerService.UpdateAsync(customerDto);

        return NoContent();
    }
}