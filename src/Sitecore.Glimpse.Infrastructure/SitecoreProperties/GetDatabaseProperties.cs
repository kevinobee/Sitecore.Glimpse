using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetDatabaseProperties(Sitecore.Data.Database db)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Database Property", "Value" },
                    new object[] { "Name", db.Name},
                    new object[] { "Connection String Name" , db.ConnectionStringName },
                    new object[] { "Read Only" , db.ReadOnly },
                    new object[] { "Protected" , db.Protected },
                    new object[] { "Security Enabled" , db.SecurityEnabled },
                    new object[] { "Proxies Enabled" , db.ProxiesEnabled },
                    new object[] { "Publish Virtual Items" , db.PublishVirtualItems },
                    new object[] { "HasContentItem" , db.HasContentItem },
                    //new object[] { "ArchiveNames" , db.ArchiveNames }
                };
            return results;
        }

    }
}
