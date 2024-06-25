using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;
using XpCase.Infrastructure.Data;

namespace XpCase.Infrastructure.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly XpCaseDbContext _context;

        public AssetRepository(XpCaseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset?>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset> GetByIdAsync(Guid id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task AddAsync(Asset? asset)
        {
            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asset asset)
        {
            var existingOrder = await _context.Assets.FindAsync(asset.AssetId);

            // Atualize as propriedades da entidade existente com os novos valores
            _context.Entry(existingOrder).CurrentValues.SetValues(asset);

            // Marque a entidade como modificada
            _context.Entry(existingOrder).State = EntityState.Modified;

            // Salve as mudanças
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AssetExistsAsync(string cod, Guid? id = null)
        {
            if (id != null)
                return await _context.Assets.AnyAsync(a => a.Symbol == cod && a.AssetId != id);

            return await _context.Assets.AnyAsync(a => a.Symbol == cod);
        }

        public async Task<Asset?> FindAsync(Expression<Func<Asset?, bool>> predicate, bool asNoTracking = false)
        {
            var query = _context.Assets.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.OrderBy(o => o.CreatedAt).FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Asset?>> FindAllAsync(Expression<Func<Asset?, bool>> predicate)
        {
            return await _context.Assets.Where(predicate).ToListAsync();
        }
    }
}
