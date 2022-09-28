using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AuthAPI.Services.Redis
{
    public static class CacheHelper
    {
        public static async Task SetRecordAsync<T> (this IDistributedCache cache, string key, T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            
            await cache.SetStringAsync(key, jsonData);
        }

        public static async Task RemoveRecordAsync (this IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }

        public static async Task<T?> GetRecordAsync<T> (this IDistributedCache cache, string key) {
            var data = await cache.GetStringAsync(key);

            if (data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
