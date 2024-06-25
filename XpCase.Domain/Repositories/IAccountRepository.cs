using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpCase.Domain.Entities;

namespace XpCase.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
    }
}
