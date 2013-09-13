using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse.Analytics
{
    public class OverviewSection
    {
        public static TabSection Create(RequestData requestData)
        {
            var section = new TabSection("Key", "Value");

            section.AddRow().Column("New vs. Returning").Column(requestData[DataKey.IsNewVisitor]);
            section.AddRow().Column("Engagement Value").Column(requestData[DataKey.EngagementValue]);
            section.AddRow().Column("Traffic Type").Column(requestData[DataKey.TrafficType]);
            section.AddRow().Column("Campaign").Column(requestData[DataKey.Campaign]);

            return section;
        }
    }
}