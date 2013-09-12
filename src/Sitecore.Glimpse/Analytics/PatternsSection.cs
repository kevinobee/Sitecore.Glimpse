using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Analytics
{
    public class PatternsSection
    {
        public static TabSection Create(IDictionary<string, object> sitecoreData)
        {
            var pattern = (Pattern) sitecoreData[DataKey.Pattern];

            if (pattern == null) return null;

            var section = new TabSection("Key", "Value");

            section.AddRow().Column("Name").Column(pattern.Name);
            section.AddRow().Column("Image").Column(pattern.Image);
            section.AddRow().Column("Description").Column(pattern.Description);

            return section;
        }
    }
}