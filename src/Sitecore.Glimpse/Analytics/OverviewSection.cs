using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse.Analytics
{
    public class OverviewSection
    {
        public static TabSection Create(IDictionary<string, object> sitecoreData)
        {
            var section = new TabSection("Key", "Value");

            section.AddRow().Column("New vs. Returning").Column(sitecoreData[DataKey.IsNewVisitor]);
            section.AddRow().Column("Engagement Value").Column(sitecoreData[DataKey.EngagementValue]);
            section.AddRow().Column("Traffic Type").Column(sitecoreData[DataKey.TrafficType]);
            section.AddRow().Column("Campaign").Column(sitecoreData[DataKey.Campaign]);

            return section;
        }
    }
}