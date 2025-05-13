using XpCase.Application.DTOs.Asset;

namespace XpCase.Application.Interfaces;

public interface IAssetService
{
    Task<IEnumerable<AssetDto>> GetAllAsync();
    Task<IEnumerable<AssetDto>> GetAssetsWithPositiveValueAndValidDateAsync();
    Task<AssetDto> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(CreateAssetDto assetDto);
    Task UpdateAsync(AssetDto assetDto);
    Task DeleteAsync(Guid id);
    Task<bool> AssetExistsAsync(string cod, Guid? id = null);
    Task<bool> HasRelatedRecordsAsync(Guid assetId);
    Task<AssetDto?> GetByIdAndPriceQuantityAndValidDateAsync(Guid id, decimal price, int quantity);
}