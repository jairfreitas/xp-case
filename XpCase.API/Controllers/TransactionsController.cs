using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XpCase.Application.DTOs.Transaction;
using XpCase.Application.Interfaces;

namespace XpCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todas as transações",
        Description = "Recupera uma lista de todas as transações.")]
    [SwaggerResponse(200, "Uma lista de transações", typeof(IEnumerable<TransactionDto>))]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
    {
        var transactions = await transactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter uma transação", Description = "Recupera uma transação específica pelo ID.")]
    [SwaggerResponse(200, "Uma transação", typeof(TransactionDto))]
    [SwaggerResponse(404, "Transação não encontrada")]
    public async Task<ActionResult<TransactionDto>> GetTransaction(Guid id)
    {
        var transaction = await transactionService.GetByIdAsync(id);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Criar uma nova transação", Description = "Cria uma nova transação.")]
    [SwaggerResponse(201, "Transação criada com sucesso", typeof(CreateTransactionDto))]
    public async Task<ActionResult<CreateTransactionDto>> PostTransaction(CreateTransactionDto transactionDto)
    {
        var transactionId = await transactionService.AddAsync(transactionDto);

        return CreatedAtAction(nameof(GetTransaction), new { id = transactionId }, transactionDto);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar uma transação", Description = "Atualiza uma transação existente.")]
    [SwaggerResponse(204, "Transação atualizada com sucesso")]
    [SwaggerResponse(400, "ID da transação não corresponde ao ID fornecido")]
    public async Task<IActionResult> PutTransaction(Guid id, TransactionDto transactionDto)
    {
        if (id != transactionDto.TransactionId) return BadRequest();

        await transactionService.UpdateAsync(transactionDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar uma transação", Description = "Deleta uma transação existente.")]
    [SwaggerResponse(204, "Transação deletada com sucesso")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        await transactionService.DeleteAsync(id);
        return NoContent();
    }
}