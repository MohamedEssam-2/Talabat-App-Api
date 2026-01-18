using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetCacheAsync(string cacheKey);
        Task SetCacheAsync(string cacheKey, string cacheValue, TimeSpan duration);
    }
}
