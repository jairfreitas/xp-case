using XpCase.Domain.Entities;

namespace XpCase.Domain.Repositories;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet?>> GetAllAsync();
    Task<Wallet?> GetByIdAsync(Guid id);
    Task AddAsync(Wallet? wallet);
    Task UpdateAsync(Wallet wallet);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Func<Wallet?, bool> predicate);
    Task<Wallet?> GetByCustomerIdAndAssetIdAsync(Guid customerId, Guid assetId);
    Task<bool> HasAssetsToOrder(Guid id, Guid assetId, int quantity);
}