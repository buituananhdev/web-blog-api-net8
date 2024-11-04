namespace WebBlog.Application.Services.Cache
{
    public interface ICacheService
    {
        Task SetCacheAsync<T>(string key, T value, TimeSpan? ttl = null);
        Task<T> GetCacheAsync<T>(string key);
        Task RemoveCacheAsync(string key);
    }
}