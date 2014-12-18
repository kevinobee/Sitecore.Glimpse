using System;
using System.Linq;

namespace Sitecore.Glimpse.Model
{
    public class SitecoreService
    {
        public string Controller { get; set; }
        public string Url { get; set; }
        public string ObjectType { get; set; }
        public string Metadata { get; set; }

        public bool IsEntityService { get; set; }

        public bool CorsEnabled
        {
            get { return Attributes.Contains("EnableCorsAttribute", StringComparer.InvariantCultureIgnoreCase); }
        }

        public string[] Attributes { get; set; }

        public SitecoreService()
        {
            Attributes = new string[]{};
        }
    }
}