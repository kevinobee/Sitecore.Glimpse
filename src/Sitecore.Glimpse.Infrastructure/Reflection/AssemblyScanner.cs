using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    internal class AssemblyScanner : ITypeProvider
    {
        private static Assembly[] _siteAssemblies;

        public IQueryable<Type> Types
        {
            get
            {
                return GetSiteAssemblies().SelectMany(GetTypes).AsQueryable();
            }
        }

        private static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return new Type[] { };
            }
        }

        private static IEnumerable<Assembly> GetSiteAssemblies()
        {
            return _siteAssemblies ?? (_siteAssemblies = AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}