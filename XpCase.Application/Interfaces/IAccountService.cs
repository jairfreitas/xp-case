using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpCase.Application.DTOs.Account;

namespace XpCase.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAsync();
    }
}
