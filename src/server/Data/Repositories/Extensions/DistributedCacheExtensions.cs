using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Data.Repositories.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T> GetObjectAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);

            return value == null
                ? default
                : JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task SetObjectAsync<T>(this IDistributedCache cache, string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, json);
        }
    }
}
