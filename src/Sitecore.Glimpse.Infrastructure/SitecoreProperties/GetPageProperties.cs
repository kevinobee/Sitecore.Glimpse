using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetPagePropertiesFull(Sitecore.Layouts.PageContext p)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Page Property", "Value" },
                    new object[] { "FilePath", p.FilePath },
                    new object[] { "Device.Name", p.Device.Name },
                    new object[] { "Device.DisplayName", p.Device.DisplayName },
                };
            return results;
        }

    }
}
