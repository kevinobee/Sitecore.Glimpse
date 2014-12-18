using System;
using System.Collections.Generic;

namespace Sitecore.Glimpse.Caching
{
    public class CachingCollectionProvider<T> : ICollectionProvider<T> where T : class
    {
        private readonly ICollectionProvider<T> _provider;
        private readonly ICache _cache;

        public CachingCollectionProvider(ICollectionProvider<T> provider, ICache cache)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (cache == null) throw new ArgumentNullException("cache");

            _provider = provider;
            _cache = cache;
        }

        private ICollection<T> GetCollection()
        {
            var cacheField = typeof(T).FullName;

            if (_cache[cacheField] == null)
            {
                _cache[cacheField] = _provider.Collection;
            }

            return _cache[cacheField] as ICollection<T>;
        }

        public ICollection<T> Collection { get { return GetCollection(); } }
    }
}