using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Analytics
{
    public class PatternsSection
    {
        public static TabSection Create(RequestData requestData)
        {
            var pattern = (Pattern) requestData[DataKey.Pattern];

            if (pattern == null) return null;

            var section = new TabSection("Key", "Value");

            section.AddRow().Column("Name").Column(pattern.Name);
            section.AddRow().Column("Image").Column(pattern.Image);
            section.AddRow().Column("Description").Column(pattern.Description);

            return section;
        }
    }
}