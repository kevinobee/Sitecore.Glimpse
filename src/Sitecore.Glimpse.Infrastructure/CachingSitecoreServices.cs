using Sitecore.Glimpse.Caching;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Infrastructure
{
    public class CachingSitecoreServices : CachingCollectionProvider<SitecoreService>
    {
        public CachingSitecoreServices(ICollectionProvider<SitecoreService> provider, ICache cache) 
            : base(provider, cache)
        {
        }
    }
}