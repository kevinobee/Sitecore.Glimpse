using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetLanguagePropertiesFull(Sitecore.Globalization.Language l)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Language Property", "Value" },
                    new object[] { "Name", l.Name},
                    new object[] { "DisplayName", l.GetDisplayName()},
                    new object[] { "CultureInfo", GetCulturePropertiesFull(l.CultureInfo)},
                    new object[] { "Origin Item Id", (l.Origin != null && l.Origin.ItemId != (Sitecore.Data.ID)null) ? l.Origin.ItemId.Guid.ToString() : string.Empty},
                };
            return results;
        }

    }
}
