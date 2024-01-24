using QuizWebAPI.Services.Interfaces;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace QuizWebAPI.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase cacheDatabase;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:");
            cacheDatabase = redis.GetDatabase();
        }

        public async Task<T?> GetData<T>(string key)
        {
            T? result = default;
            var value = await cacheDatabase.StringGetAsync(key);
            if(!string.IsNullOrEmpty(value))
            {
                result = JsonSerializer.Deserialize<T>(value);
            }
            return result;
        }

        public async Task<object?> RemoveData(string key)
        {
            var exists = cacheDatabase.KeyExists(key);
            if(exists)
            {
                return await cacheDatabase.KeyDeleteAsync(key);
            }
            return null;
        }

        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var setValueTask = Task.Factory.StartNew(() =>
            {
                return cacheDatabase.StringSet(key, JsonSerializer.Serialize(value), expireTime);
            });
            return await setValueTask;
        }
    }
}
