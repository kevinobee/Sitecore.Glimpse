using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    internal class ControllerAssemblyScanner : ITypeProvider
    {
        public Type[] Types { get { return GetControllers().ToArray(); } }

        private static Assembly[] _siteAssemblies;

        private IEnumerable<Type> GetControllers()
        {
            return GetSiteAssemblies()
                    .SelectMany(assembly => GetTypes(assembly)
                    .Where(IsServicesController));
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

        private bool IsServicesController(Type type)
        {
            if (type.IsAbstract) return false;

            return 
                ServicesControllerAttribute.IsPresentOn(type) ||
                ServicesApiController.IsServicesController(type);
        }

        private static IEnumerable<Assembly> GetSiteAssemblies()
        {
            return _siteAssemblies ?? (_siteAssemblies = AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}