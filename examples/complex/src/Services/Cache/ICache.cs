using System;
using System.Threading.Tasks;

namespace TinyFpTest.Services
{
    public interface ICache
    {
        Task<bool> ExistsAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? ttl = null);
    }
}
