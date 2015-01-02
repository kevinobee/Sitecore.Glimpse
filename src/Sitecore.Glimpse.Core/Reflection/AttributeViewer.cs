using System;
using Sitecore.Glimpse.Extensions;

namespace Sitecore.Glimpse.Reflection
{
    public class AttributeViewer
    {
        private readonly Type _type;
        private readonly Func<Type, bool> _isRootAttribute;

        public AttributeViewer(Type type, Func<Type, bool> isRootAttribute)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (isRootAttribute == null) throw new ArgumentNullException("isRootAttribute");

            _type = type;
            _isRootAttribute = isRootAttribute;
        }

        public string Name
        {
            get { return _type.Name.RemoveFromEnd("Attribute"); }
        }

        public AttributeViewer Base
        {
            get
            {
                return !_isRootAttribute(_type.BaseType) 
                            ? new AttributeViewer(_type.BaseType, _isRootAttribute) 
                            : null;
            }
        }

        public bool ShouldSerializeBase()
        {
            return Base != null;
        }
    }
}