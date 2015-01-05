using System;
using Sitecore.Glimpse.Extensions;
using Sitecore.Glimpse.Infrastructure.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    public class AttributeViewer
    {
        private readonly Type _type;

        public AttributeViewer(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            _type = type;
        }

        public string Name
        {
            get { return _type.Name.RemoveFromEnd("Attribute"); }
        }

        public Type UnderlyingType
        {
            get
            {
                return _type;
            }
        }

        public AttributeViewer Base
        {
            get
            {
                return !_type.BaseType.IsRootAttribute()
                            ? new AttributeViewer(_type.BaseType) 
                            : null;
            }
        }

        public bool ShouldSerializeBase()
        {
            return Base != null;
        }
    }
}