using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpCase.Application.DTOs.Account;
using XpCase.Application.Interfaces;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;

namespace XpCase.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }
    }
}
