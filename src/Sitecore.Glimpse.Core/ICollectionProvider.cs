using System.Collections.Generic;

namespace Sitecore.Glimpse
{
    public interface ICollectionProvider<T> where T : class
    {
        ICollection<T> Collection { get; }
    }
}