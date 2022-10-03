using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace FederatedIdentityDemo.Shared.Services.Redis
{
    public static class CacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string key, T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
                SlidingExpiration = TimeSpan.FromDays(1),
            };

            await cache.SetStringAsync(key, jsonData, options);
        }

        public static async Task RemoveRecordAsync(this IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }

        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string key)
        {
            var data = await cache.GetStringAsync(key);

            if (data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
