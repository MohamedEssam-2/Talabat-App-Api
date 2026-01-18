using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetCacheAsync(string cacheKey);
        Task SetCacheAsync(string cacheKey, object cacheValue, TimeSpan duration);
    }
}
