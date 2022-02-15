using TinyFp;

namespace TinyFpTest.Services
{
    public class Cache : ICache
    {
        public Task<bool> ExistsAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Option<T>> GetAsync<T>(string key)
            => Task.FromResult(Option<T>.None);

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            throw new NotImplementedException();
        }
    }
}
