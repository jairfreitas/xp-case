using AutoMapper;
using XpCase.Application.DTOs.Customer;
using XpCase.Application.Interfaces;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;

namespace XpCase.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task UpdateAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task<bool> CustomerExistsAsync(string email, Guid id)
    {
        return await _customerRepository.CustomerExistsAsync(email, id);
    }

    public async Task<bool> HasAmountToOrder(Guid id, int quantity, decimal price)
    {
        return await _customerRepository.HasAmountToOrder(id, quantity, price);
    }
}