using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;
using XpCase.Infrastructure.Data;

namespace XpCase.Infrastructure.Repositories;

public class OrderRepository(XpCaseDbContext context) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task AddAsync(Order? order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        var existingOrder = await context.Orders.FindAsync(order.OrderId);

        // Atualize as propriedades da entidade existente com os novos valores
        context.Entry(existingOrder).CurrentValues.SetValues(order);

        // Marque a entidade como modificada
        context.Entry(existingOrder).State = EntityState.Modified;

        // Salve as mudanças
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order != null)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Order> FindAsync(Expression<Func<Order, bool>> predicate, bool asNoTracking = false)
    {
        var query = context.Orders.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.OrderBy(o => o.CreatedAt).FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Order>> FindAllAsync(Expression<Func<Order, bool>> predicate,
        bool asNoTracking = false)
    {
        //return context.Orders.Where(predicate).OrderBy(o => o.CreatedAt).ToList();

        var query = context.Orders.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.Where(predicate).OrderBy(o => o.CreatedAt).ToListAsync();
    }

    public async Task<bool> AnyAsync(Func<Order?, bool> predicate)
    {
        return await Task.FromResult(context.Orders.Any(predicate));
    }
}