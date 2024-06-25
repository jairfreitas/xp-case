using Microsoft.EntityFrameworkCore;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;
using XpCase.Infrastructure.Data;

namespace XpCase.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly XpCaseDbContext _context;

    public CustomerRepository(XpCaseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }


    public async Task UpdateAsync(Customer? customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CustomerExistsAsync(string email, Guid id)
    {
        return await _context.Customers.AnyAsync(c => c.Email == email && c.CustomerId != id);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<bool> HasAmountToOrder(Guid id, int quantity, decimal price)
    {
        var customer = await _context.Customers.FindAsync(id);

        var totalOrderValue = quantity * price;

        return customer?.Amount >= totalOrderValue;
    }
}