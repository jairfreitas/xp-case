using XpCase.Application.DTOs.Transaction;

namespace XpCase.Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllAsync();
    Task<TransactionDto> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(CreateTransactionDto transactionDto);
    Task UpdateAsync(TransactionDto transactionDto);
    Task DeleteAsync(Guid id);
}