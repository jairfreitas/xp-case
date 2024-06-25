using XpCase.Application.DTOs.Customer;

namespace XpCase.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task UpdateAsync(CustomerDto customerDto);
    Task<bool> CustomerExistsAsync(string email, Guid id);
    Task<bool> HasAmountToOrder(Guid id, int quantity, decimal price);
}