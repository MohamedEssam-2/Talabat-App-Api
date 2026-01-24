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
        public async Task<string?> GetCacheAsync(string cacheKey)
        {
           var cacheValue = await  _cacheRepository.GetCacheAsync(cacheKey);
            return cacheValue;
        }

        public  Task SetCacheAsync(string cacheKey, object cacheValue, TimeSpan duration)
        {
            var valueToReturn = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return _cacheRepository.SetCacheAsync(cacheKey, valueToReturn, duration);
        }
    }
}
