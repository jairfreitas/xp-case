using XpCase.Application.DTOs.Account;

namespace XpCase.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAllAsync();
}