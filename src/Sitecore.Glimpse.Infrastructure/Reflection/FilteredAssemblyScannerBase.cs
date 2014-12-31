using System;
using System.Linq;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    internal abstract class FilteredAssemblyScannerBase : ITypeProvider
    {
        private readonly ITypeProvider _typeProvider;

        protected FilteredAssemblyScannerBase(ITypeProvider typeProvider)
        {
            _typeProvider = typeProvider;
        }

        public IQueryable<Type> Types
        {
            get
            {
                return _typeProvider.Types.Where(Filter).AsQueryable();
            }
        }

        protected abstract bool Filter(Type type);
    }
}