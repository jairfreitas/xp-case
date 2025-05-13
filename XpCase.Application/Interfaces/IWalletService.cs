using XpCase.Application.DTOs.Wallet;
using XpCase.Domain.Entities;

namespace XpCase.Application.Interfaces;

public interface IWalletService
{
    Task<IEnumerable<WalletDto>> GetAllAsync();
    Task<WalletDto> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(CreateWalletDto walletDto);
    Task UpdateAsync(WalletDto walletDto);
    Task DeleteAsync(Guid id);
    Task<Wallet?> GetByCustomerIdAndAssetIdAsync(Guid customerId, Guid assetId);
    Task<bool> HasAssetsToOrder(Guid id, Guid assetId, int quantity);
}