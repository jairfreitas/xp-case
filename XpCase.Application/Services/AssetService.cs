using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using XpCase.Application.DTOs.Asset;
using XpCase.Application.Interfaces;
using XpCase.Domain.Entities;
using XpCase.Domain.Repositories;

namespace XpCase.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IWalletRepository _walletRepository;
        private readonly IOrderRepository _orderRepository;

        private const string AssetListCacheKey = "assetList";
        private const string AssetListWithQuantityGreaterThanZeroCacheKey = "assetListWithQuantityGreaterThanZero";

        public AssetService(
            IAssetRepository assetRepository,
            IMapper mapper,
            IMemoryCache cache,
            IWalletRepository walletRepository,
            IOrderRepository orderRepository)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _cache = cache;
            _walletRepository = walletRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<AssetDto>> GetAllAsync()
        {
            if (!_cache.TryGetValue(AssetListCacheKey, out IEnumerable<AssetDto> assetList))
            {
                var assets = await _assetRepository.GetAllAsync();
                assetList = _mapper.Map<IEnumerable<AssetDto>>(assets);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _cache.Set(AssetListCacheKey, assetList, cacheEntryOptions);
            }
            return assetList;
        }

        public async Task<IEnumerable<AssetDto>> GetAssetsWithPositiveValueAndValidDateAsync()
        {
            if (!_cache.TryGetValue(AssetListWithQuantityGreaterThanZeroCacheKey, out IEnumerable<AssetDto> assetList))
            {
                var assets = await _assetRepository.FindAllAsync(a => a.Quantity > 0 && a.ExpirationDate >= DateTime.Now);

                assetList = _mapper.Map<IEnumerable<AssetDto>>(assets);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _cache.Set(AssetListWithQuantityGreaterThanZeroCacheKey, assetList, cacheEntryOptions);
            }
            return assetList;
        }

        public async Task<AssetDto> GetByIdAsync(Guid id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<Guid> AddAsync(CreateAssetDto assetDto)
        {
            var asset = _mapper.Map<Asset>(assetDto);

            await _assetRepository.AddAsync(asset);

            // Invalidate cache
            _cache.Remove(AssetListCacheKey);
            _cache.Remove(AssetListWithQuantityGreaterThanZeroCacheKey);

            return asset.AssetId;
        }

        public async Task UpdateAsync(AssetDto assetDto)
        {
            var asset = _mapper.Map<Asset>(assetDto);

            await _assetRepository.UpdateAsync(asset);

            // Invalidate cache
            _cache.Remove(AssetListCacheKey);
            _cache.Remove(AssetListWithQuantityGreaterThanZeroCacheKey);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await HasRelatedRecordsAsync(id))
            {
                throw new InvalidOperationException("Não é possível deletar esse Asser porque está relacionado a um Wallets ou Orders.");
            }

            await _assetRepository.DeleteAsync(id);

            // Invalidate cache
            _cache.Remove(AssetListCacheKey);
            _cache.Remove(AssetListWithQuantityGreaterThanZeroCacheKey);
        }

        public async Task<bool> AssetExistsAsync(string cod, Guid? id = null)
        {
            if (id != null)
                return await _assetRepository.AssetExistsAsync(cod, id);

            return await _assetRepository.AssetExistsAsync(cod);
        }

        public async Task<bool> HasRelatedRecordsAsync(Guid assetId)
        {
            var hasWallets = await _walletRepository.AnyAsync(w => w.AssetId == assetId);
            var hasOrders = (await _orderRepository.AnyAsync(o => o.AssetId == assetId));
            return hasWallets || hasOrders;
        }

        public async Task<AssetDto?> GetByIdAndPriceQuantityAndValidDateAsync(Guid id, decimal price, int quantity)
        {
            var asset = await _assetRepository.FindAsync(a => a.AssetId == id && a.Price <= price && a.Quantity >= quantity && a.ExpirationDate >= DateTime.Now, true);

            return _mapper.Map<AssetDto>(asset);
        }
    }
}
