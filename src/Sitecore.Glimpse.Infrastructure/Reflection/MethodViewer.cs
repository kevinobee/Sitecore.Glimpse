using System;
using System.Linq;
using System.Reflection;
using Sitecore.Glimpse.Infrastructure.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Reflection
{
    public class MethodViewer
    {
        private readonly MethodInfo _methodInfo;

        public MethodViewer(MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException("methodInfo");

            _methodInfo = methodInfo;
        }

        public string Name { get { return _methodInfo.Name; } }

        public AttributeViewer[] Attributes 
        { 
            get
            {
                return _methodInfo.GetCustomAttributes(false)
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
    }
}