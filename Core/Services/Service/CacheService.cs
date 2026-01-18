using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Services_Abstraction;

namespace Services_Layer.Service
{
    public class CacheService (ICacheRepository _cacheRepository) : ICacheService
    {
        public Task<string?> GetCacheAsync(string cacheKey)
        {
           var cacheValue = _cacheRepository.GetCacheAsync(cacheKey);
            return cacheValue;
        }

        public Task SetCacheAsync(string cacheKey, string cacheValue, TimeSpan duration)
        {
            var valueToReturn = JsonSerializer.Serialize(cacheValue);
            return _cacheRepository.SetCacheAsync(cacheKey, valueToReturn, duration);
        }
    }
}
