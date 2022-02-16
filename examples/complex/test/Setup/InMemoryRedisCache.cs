using System.Collections.Concurrent;
using TinyFp.Extensions;
using TinyFpTest.Services;

namespace TinyFp.Complex.Setup
{
    public class InMemoryRedisCache : ICache
    {
        private static readonly ConcurrentDictionary<string, object> _inMemoryCache = new ConcurrentDictionary<string, object>();

        public Task<bool> ExistsAsync(string key)
            => Task.FromResult(_inMemoryCache.ContainsKey(key));

        public Task<Option<T>> GetAsync<T>(string key)
            => _inMemoryCache.ContainsKey(key) ? 
                Task.FromResult(Option<T>.Some((T)_inMemoryCache[key])) : 
                Task.FromResult(Option<T>.None());

        public Task<bool> RemoveAsync(string key)
            => Task.FromResult(
                _inMemoryCache.TryRemove(key, out var removed)
                .Map(_ => removed)
                .ToOption()
                .Match(ok => true, () => false));

        public Task<bool> SetAsync<T>(string key, T value, TimeSpan? ttl = null)
            => Task.Run(() => true.Tee(_ => _inMemoryCache.TryAdd(key, value)));

        public void ClearCache()
            => _inMemoryCache.Clear();
    }
}
