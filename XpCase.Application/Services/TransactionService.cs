using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpCase.Application.DTOs.Order;
using XpCase.Application.DTOs.Transaction;
using XpCase.Application.Interfaces;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;

namespace XpCase.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<Guid> AddAsync(CreateTransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            await _transactionRepository.AddAsync(transaction);

            return transaction.TransactionId;
        }

        public async Task UpdateAsync(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            await _transactionRepository.UpdateAsync(transaction);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
        }
    }
}
