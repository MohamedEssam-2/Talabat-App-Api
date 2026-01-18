using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace Presistence_Layer.Repositories
{
    public class CacheRepository (IConnectionMultiplexer connection): ICacheRepository
    {
        private readonly StackExchange.Redis.IDatabase _database= connection.GetDatabase();
        public async Task<string?> GetCacheAsync(string cacheKey)
        {
            var cachevalue= await _database.StringGetAsync(cacheKey);
            return (cachevalue.IsNullOrEmpty) ? null : cachevalue.ToString();
        }

        public async Task SetCacheAsync(string cacheKey, string cacheValue, TimeSpan duration)
        {
             await _database.StringSetAsync(cacheKey, cacheValue, duration);
        }
    }
}
