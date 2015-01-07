using System.Collections.Generic;
using Sitecore.Glimpse.Infrastructure.Reflection;

namespace Sitecore.Glimpse.Infrastructure.Extensions
{
    public static class AttributeViewerExtensions
    {
        public static bool ContainsType(this IEnumerable<AttributeViewer> attributes, string typeName)
        {
            foreach (var attribute in attributes)
            {
                if (attribute.UnderlyingType.Name == typeName)
                {
                    return true;
                }

                var attributeBase = attribute.Base;

                while (attributeBase != null)
                {
                    if (attributeBase.UnderlyingType.Name == typeName)
                    {
                        return true;
                    }

                    attributeBase = attributeBase.Base;
                }
            }

            return false;
        }

    }
}