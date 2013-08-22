using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetRequestPropertiesFull(Sitecore.Sites.SiteRequest r)
        {
            var queryString = new List<object[]>()
            {
                new object[] { "key", "value" }
            };
            
            foreach (string key in r.QueryString.AllKeys)
            {
                queryString.Add(new object[] { key, r.QueryString.GetValues(key) });
            }

            var results = new List<object[]>()
                {
                    new object[] { "Request Property", "Value" },
                    new object[] { "FilePath", r.FilePath},
                    new object[] { "ItemPath", r.ItemPath},
                    new object[] { "QueryString", queryString}
                };
            return results;
        }

    }
}
