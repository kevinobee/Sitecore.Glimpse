using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetDevicePropertiesFull(Sitecore.Data.Items.DeviceItem di)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Device Property", "Value" },
                    new object[] { "Name", di.Name},
                    new object[] { "Display Name", di.DisplayName},
                    new object[] { "Id", di.ID.Guid},
                    new object[] { "Query String", di.QueryString},
                    new object[] { "Agent", di.Agent},
                    new object[] { "Fallback Device Name", (di.FallbackDevice!=null) ? di.FallbackDevice.Name : string.Empty },
                    new object[] { "Icon", di.Icon},
                    new object[] { "Is Default", di.IsDefault},
                    new object[] { "Is Valid", di.IsValid},
                    // new object[] { "Capabilities.Browser", di.Capabilities.Browser},
                    // new object[] { "Properties", di.Capabilities.Properties.AllKeys}
                    // new object[] { "ArchiveNames" , db.ArchiveNames }
                };
            return results;
        }

    }
}
