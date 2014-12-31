using System;
using System.Linq;

namespace Sitecore.Glimpse
{
    public interface ITypeProvider  // TODO use this
    {
        IQueryable<Type> Types { get; }
    }
}