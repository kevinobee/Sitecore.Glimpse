using System;
using System.Linq;
using System.Reflection;

namespace Sitecore.Glimpse.Reflection
{
    public class MethodViewer
    {
        private readonly MethodInfo _methodInfo;
        private readonly Func<Type, bool> _isRootAttribute;

        public MethodViewer(MethodInfo methodInfo, Func<Type, bool> isRootAttribute)
        {
            if (methodInfo == null) throw new ArgumentNullException("methodInfo");
            if (isRootAttribute == null) throw new ArgumentNullException("isRootAttribute");

            _methodInfo = methodInfo;
            _isRootAttribute = isRootAttribute;
        }

        public string Name { get { return _methodInfo.Name; } }

        public AttributeViewer[] Attributes 
        { 
            get
            {
                return _methodInfo.GetCustomAttributes(false)
                    .Select(x => x.GetType())
                    .Where(x => !_isRootAttribute(x))
                    .Select(x => new AttributeViewer(x, _isRootAttribute))
                    .ToArray();
            } 
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Length > 0;
        }
    }
}