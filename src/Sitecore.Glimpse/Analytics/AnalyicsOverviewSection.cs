using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse.Analytics
{
    public class AnalyicsOverviewSection : BaseSection
    {
        public AnalyicsOverviewSection(RequestData requestData) : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var isNewVisitor = RequestData[DataKey.IsNewVisitor];
            var engagementValue = RequestData[DataKey.EngagementValue];
            var trafficType = RequestData[DataKey.TrafficType];

            if ((isNewVisitor == null) || (engagementValue == null) || (trafficType == null)) return null;

            var section = new TabSection("Overview", "Value");

            section.AddRow().Column("New vs. Returning").Column(isNewVisitor);
            section.AddRow().Column("Engagement Value").Column(engagementValue);
            section.AddRow().Column("Traffic Type").Column(trafficType);

            var campaign = RequestData[DataKey.Campaign];
            if (campaign != null) section.AddRow().Column("Campaign").Column(campaign);

            return section;
        }
    }
}