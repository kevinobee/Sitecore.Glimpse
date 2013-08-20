using System;

using Glimpse.Core.Extensibility;

namespace Sitecore.Glimpse
{
    public class SitecoreTab : ITab
    {
        public SitecoreTab()
        {
            Name = "Sitecore";
        }

        public object GetData(ITabContext context)
        {
            throw new NotImplementedException();
        }

        public string Name { get; private set; }
        public RuntimeEvent ExecuteOn { get; private set; }
        public Type RequestContextType { get; private set; }
    }
}