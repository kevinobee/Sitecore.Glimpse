using System;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Infrastructure;

namespace Sitecore.Glimpse.Analytics
{
    public class SitecoreAnalyticsTab : TabBase
    {
        private readonly ISitecoreRequest _sitecoreRequest;

        public SitecoreAnalyticsTab()
            : this(new SitecoreAnalyticsForRequest())
        {
        }

        public SitecoreAnalyticsTab(ISitecoreRequest sitecoreRequest)
        {
            _sitecoreRequest = sitecoreRequest;
        }

        public override object GetData(ITabContext context)
        {
            try
            {
                dynamic sitecoreData = _sitecoreRequest.GetData();

                var plugin = Plugin.Create("Property", "Value");

                plugin.AddRow().Column("New vs. Returning").Column(sitecoreData[DataKey.IsNewVisitor]);
                plugin.AddRow().Column("Engagement Value").Column(sitecoreData[DataKey.EngagementValue]);
                plugin.AddRow().Column("Traffic Type").Column(sitecoreData[DataKey.TrafficType]);
                plugin.AddRow().Column("Campaign").Column(sitecoreData[DataKey.Campaign]);

                TabSection goalsSection = GoalsSection.Create(sitecoreData);
                TabSection pageViewsSection = PageViewsSection.Create(sitecoreData);
                TabSection patternsSection = PatternsSection.Create(sitecoreData);
                TabSection profilesSection = ProfilesSection.Create(sitecoreData);

                if (goalsSection != null) 
                    plugin.Section("Goals", goalsSection);

                if (pageViewsSection != null) 
                    plugin.Section("Page Views", pageViewsSection);

                if (patternsSection != null) 
                    plugin.Section("Pattern", patternsSection);

                if (profilesSection != null) 
                    plugin.Section("Profiles", profilesSection);

                return plugin;
            }
            catch (Exception ex)
            {
                return new { Exception = ex };
            }
        }

        public override string Name
        {
            get { return "Sitecore Analytics"; }
        }
    }
}