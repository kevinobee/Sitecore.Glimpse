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
                typeof(ActionNameAttribute),
                typeof(AuthorizationFilterAttribute),
                typeof(System.Web.Mvc.ActionFilterAttribute),
                typeof(System.Web.Mvc.AcceptVerbsAttribute),
                typeof(System.Web.Mvc.FilterAttribute),
                typeof(System.Web.Mvc.ActionMethodSelectorAttribute)
            };

            return ((type.BaseType == null) || (rootTypes.Contains(type)));
        }

        public static bool IsRootType(Type type)
        {
            var rootTypes = new[]
            {
                typeof(object), 
                typeof(System.Web.Http.ApiController), 
                typeof(ServicesApiController), 
                typeof(EntityServiceBase<>),
                typeof(System.Web.Mvc.Controller)
            };

            return ((type.BaseType == null) || (rootTypes.Any(x => x.Name == type.Name)) || (rootTypes.Any(x => x.Name == type.BaseType.Name)));
        }
    }
}