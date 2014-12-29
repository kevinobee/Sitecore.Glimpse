using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using Sitecore.Services.Infrastructure.Services;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Glimpse.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                       .Where(i => i.IsGenericType)
                       .FirstOrDefault(i => i.GetGenericTypeDefinition() == interfaceType);
        }

        public static bool IsRootAttribute(Type type)
        {
            var rootTypes = new[]
            {
                typeof(object), 
                typeof(Attribute), 
                typeof(FilterAttribute),
                typeof(ActionFilterAttribute),
                typeof(ActionNameAttribute)
            };

            return ((type.BaseType == null) || (rootTypes.Contains(type)));
        }

        public static bool IsRootType(Type type)
        {
            var rootTypes = new[]
            {
                typeof(object), 
                typeof(ServicesApiController), 
                typeof(EntityServiceBase<>)
            };

            return ((type.BaseType == null) || (rootTypes.Any(x => x.Name == type.BaseType.Name)));
        }
    }
}