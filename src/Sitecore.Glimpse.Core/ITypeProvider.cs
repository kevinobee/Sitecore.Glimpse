using System;
using System.Linq;

namespace Sitecore.Glimpse
{
    public interface ITypeProvider
    {
        IQueryable<Type> Types { get; }
    }
}