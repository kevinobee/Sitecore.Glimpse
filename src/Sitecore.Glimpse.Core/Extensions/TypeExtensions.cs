using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Glimpse.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<string> GetAttributes(this Type type)
        {
            return type.GetCustomAttributes(true)
                       .Select(attribute => attribute.GetType().Name);
        }

        public static Type GetGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                       .Where(i => i.IsGenericType)
                       .FirstOrDefault(i => i.GetGenericTypeDefinition() == interfaceType);
        }
    }
}