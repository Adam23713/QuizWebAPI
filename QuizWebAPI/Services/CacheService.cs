using Microsoft.Extensions.Configuration;
using QuizWebAPI.Services.Interfaces;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace QuizWebAPI.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConfiguration configuration;
        private readonly IDatabase cacheDatabase;

        public CacheService(IConfiguration configuration)
        {
            if(configuration == null)
            {  
                throw new ArgumentNullException(nameof(configuration));
            }
            this.configuration = configuration;
            var redis = ConnectionMultiplexer.Connect(configuration["Cache:Redis"]);
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
