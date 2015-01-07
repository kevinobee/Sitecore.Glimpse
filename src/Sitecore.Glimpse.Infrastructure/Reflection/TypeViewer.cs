using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

using Sitecore.Glimpse.Infrastructure.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    public class TypeViewer
    {
        private readonly Type _type;

        public TypeViewer(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            _type = type;
        }

        public string Name
        {
            get { return _type.FullName; }
        }

        public TypeViewer Base
        {
            get
            {
                return !_type.IsRootType()
                    ? new TypeViewer(_type.BaseType)
                    : null;
            }
        }

        public bool ShouldSerializeBase()
        {
            return Base != null;
        }

        public AttributeViewer[] Attributes
        {
            get
            {
                return _type.GetCustomAttributes(false)
                    .Select(x => x.GetType())
                    .Where(x => ! x.IsRootAttribute())
                    .Select(x => new AttributeViewer(x))
                    .ToArray();
            }
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Length > 0;
        }

        public MethodViewer[] Methods
        {
            get
            {
                return _type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(mi => !IsPropertyAccessor(mi))
                    .Select(mi => new MethodViewer(mi))
                    .ToArray();
            }
        }

        public bool ShouldSerializeMethods()
        {
            return Methods.Length > 0;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        private static bool IsPropertyAccessor(MethodInfo methodInfo)
        {
            return methodInfo.IsSpecialName &&
                   (methodInfo.Name.StartsWith("set_") || methodInfo.Name.StartsWith("get_"));
        }

        public bool HasClassAttribute(string typeName)
        {
            if (Attributes.ContainsType(typeName))
            {
                return true;
            }

            return Base != null && Base.HasClassAttribute(typeName);
        }

        public bool HasMethodAttribute(string typeName)
        {
            if (Methods.Any(method => method.Attributes.ContainsType(typeName)))
            {
                return true;
            }

            return Base != null && Base.HasMethodAttribute(typeName);
        }
    }
}