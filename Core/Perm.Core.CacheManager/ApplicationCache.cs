namespace Perm.Core.CacheManager
{
    public class ApplicationCache
    {
        private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        private static readonly object _setLock = new();
        private static readonly object _getLock = new();
        public static void Set(string key, object value)
        {
            lock (_setLock)
            {
                _cache[key] = value;
            }
        }

        public static object Get(string key)
        {
            lock (_getLock)
            {
                if (_cache.ContainsKey(key))
                {
                    return _cache[key];
                }
            }

            return null;
        }
    }
}