using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XpCase.Application.DTOs.Account;
using XpCase.Application.Interfaces;

namespace XpCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Endpoints para gerenciar contas de operação")]
public class AccountsController(IAccountService accountService) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(200, "Lista de contas obtida com sucesso", typeof(IEnumerable<AccountDto>))]
    [SwaggerOperation(Summary = "Obter todos os usuários da operação",
        Description = "Retorna uma lista de todos os usuários da operação.")]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
    {
        var accounts = await accountService.GetAllAsync();
        return Ok(accounts);
    }
}