using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetCulturePropertiesFull(System.Globalization.CultureInfo c)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Culture Property", "Value" },
                    new object[] { "Name", c.Name},
                    new object[] { "Parent", c.Parent},
                    new object[] { "Display Name", c.DisplayName},
                    new object[] { "English Name", c.EnglishName},
                    new object[] { "Native Name", c.NativeName},
                    new object[] { "Two Letter ISO Language Name", c.TwoLetterISOLanguageName},
                    new object[] { "Three Letter Windows Language Name", c.ThreeLetterWindowsLanguageName},
                    new object[] { "Three Letter ISO Language Name", c.ThreeLetterISOLanguageName}
                };
            return results;
        }

    }
}
