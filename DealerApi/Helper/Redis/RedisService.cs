using Newtonsoft.Json;
using StackExchange.Redis;

namespace DealerApi.Helper.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _redis;

        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _redis.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;

            return JsonConvert.DeserializeObject<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            await _redis.StringSetAsync(key, jsonData, expiry);
        }

        public async Task RemoveAsync(string key)
        {
            await _redis.KeyDeleteAsync(key);
        }
    }

}
