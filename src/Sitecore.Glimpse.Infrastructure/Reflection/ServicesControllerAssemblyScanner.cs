using System;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    internal class ServicesControllerAssemblyScanner : FilteredAssemblyScannerBase
    {
        public ServicesControllerAssemblyScanner(ITypeProvider typeProvider) 
            : base(typeProvider)
        {
        }

        protected override bool Filter(Type type)
        {
            if (type.IsAbstract) return false;

            return 
                ServicesControllerAttribute.IsPresentOn(type) ||
                ServicesApiController.IsServicesController(type);
        }
    }
}