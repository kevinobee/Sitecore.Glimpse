using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetSiteProperties(Sitecore.Sites.SiteContext site)
        {
            var results = new List<object[]>()
                {
                    
                    new object[] { "Site Property", "Value" },
                    new object[] { "Name", site.Name},
                    new object[] { "HostName" , site.HostName },
                    new object[] { "TargetHostName" , site.TargetHostName },
                    new object[] { "Language" , site.Language },
                    new object[] { "Database" , site.Properties["database"] },
                    new object[] { "Device" , site.Device },
                    new object[] { "RootPath" , site.RootPath },
                    new object[] { "StartItem" , site.StartItem },
                    new object[] { "StartPath" , site.StartPath },
                    new object[] { "PhysicalFolder" , site.PhysicalFolder },
                    new object[] { "VirtualFolder" , site.VirtualFolder },
                    new object[] { "LoginPage" , site.LoginPage },
                    new object[] { "RequireLogin" , site.RequireLogin },
                    new object[] { "AllowDebug" , site.AllowDebug },
                    new object[] { "EnableAnalytics" , site.EnableAnalytics },
                    new object[] { "EnableDebugger" , site.EnableDebugger },
                    new object[] { "EnablePreview" , site.EnablePreview},
                    new object[] { "EnableWorkflow" , site.EnableWorkflow },
                    new object[] { "EnableWebEdit" , site.EnableWebEdit },
                    new object[] { "FilterItems" , site.FilterItems },
                    new object[] { "CacheHtml" , site.CacheHtml },
                    new object[] { "CacheMedia" , site.CacheMedia },
                    new object[] { "MediaCachePath" , site.MediaCachePath },
                    new object[] { "XmlControlPage" , site.XmlControlPage }
                };
            return results;

        }

    }
}
