using Domain.Contract;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICashingRepository _cashingRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await _cashingRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cachekey, string cacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue);
            await _cashingRepository.SetAsync(cachekey, value, timeToLive);
        }
    }
}
