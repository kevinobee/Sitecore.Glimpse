using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Sitecore.Glimpse.Reflection
{
    public class TypeViewer
    {
        private readonly Type _type;
        private readonly Func<Type, bool> _isRootType;
        private readonly Func<Type, bool> _isRootAttribute;

        public TypeViewer(Type type, Func<Type, bool> isRootType, Func<Type, bool> isRootAttribute)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (isRootType == null) throw new ArgumentNullException("isRootType");
            if (isRootAttribute == null) throw new ArgumentNullException("isRootAttribute");

            _type = type;
            _isRootType = isRootType;
            _isRootAttribute = isRootAttribute;
        }
        
        public string Name
        {
            get
            {
                return _type.FullName;
            }
        }

        public TypeViewer Base
        {
            get
            {
                return !_isRootType(_type) 
                            ? new TypeViewer(_type.BaseType, _isRootType, _isRootAttribute) 
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
                                  .Where(x => ! _isRootAttribute(x))
                                  .Select(x => new AttributeViewer(x, _isRootAttribute))
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
                            .Select(mi => new MethodViewer(mi, _isRootAttribute))
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
    }
}