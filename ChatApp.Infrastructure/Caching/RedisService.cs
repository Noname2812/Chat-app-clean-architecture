using ChatApp.Application.Abstractions.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ChatApp.Infrastructure.Caching
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public RedisService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }
        public async Task<string?> GetDataByKey(string key)
        {
            var res = await _distributedCache.GetStringAsync(key);
            return string.IsNullOrEmpty(res) ? "" : JsonConvert.DeserializeObject<string>(res);
        }


        public async Task RemoveDataByKey(string key)
        {
            var res = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(res))
            {
                await _distributedCache.RemoveAsync(key);
            }
        }

        public async Task SetData(string key, object data, TimeSpan exprationTime)
        {
            if (data != null)
            {
                var serializerRespone = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await _distributedCache.SetStringAsync(key, serializerRespone, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = exprationTime,
                });
            }
        }

        public async Task<List<T>?> GetDataByEndpoint<T>(string endpoint)
        {
            var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: "list-users-online:*");
            var values = new List<T>();
            foreach (var key in keys)
            {
                var value = await _distributedCache.GetStringAsync(key);
                if (!string.IsNullOrEmpty(value))
                {
                    var item = JsonConvert.DeserializeObject<T>(value);
                    values.Add(item);
                }
            }
            return values;
        }
    }
}
