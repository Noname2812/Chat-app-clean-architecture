namespace ChatApp.Application.Abstractions.Services
{
    public interface IRedisService
    {
        Task<string?> GetDataByKey(string key);
        Task<List<T>?> GetDataByEndpoint<T>(string endpoint);
        Task SetData(string key, object data, TimeSpan time);
        Task RemoveDataByKey(string key);
    }
}
