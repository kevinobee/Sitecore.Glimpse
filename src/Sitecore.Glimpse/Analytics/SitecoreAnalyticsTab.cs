using System;

using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Infrastructure;

namespace Sitecore.Glimpse.Analytics
{
    public class SitecoreAnalyticsTab : TabBase, IDocumentation
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
                var sitecoreData = _sitecoreRequest.GetData();

                if (sitecoreData == null) return null;

                var analticsSummary = new AnalticsSummary(sitecoreData).Create();

                if (string.IsNullOrEmpty(analticsSummary)) return null;

                var plugin = Plugin.Create("Visitor", analticsSummary);

                var analyicsOverviewSection = new AnalyicsOverviewSection(sitecoreData).Create();
                var goalsSection = new GoalsSection(sitecoreData).Create();
                var pageViewsSection = new PageViewsSection(sitecoreData).Create();

                if (analyicsOverviewSection != null)
                    plugin.AddRow().Column("Overview").Column(analyicsOverviewSection).Selected();

                if (goalsSection != null)
                    plugin.AddRow().Column("Goals").Column(goalsSection).Info();

                if (pageViewsSection != null)
                    plugin.AddRow().Column("Page Views").Column(pageViewsSection).Quiet();


                // TODO reinstate the profile and pattern sections to run with generic DMS implementations rather than Officecore specific IDs

//                var patternsSection = PatternsSection.Create(sitecoreData);
//                var profilesSection = ProfilesSection.Create(sitecoreData);
//
//
//                if (patternsSection != null) 
//                    plugin.Section("Pattern", patternsSection);
//
//                if (profilesSection != null) 
//                    plugin.Section("Profiles", profilesSection);

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

        public string DocumentationUri { get { return Constants.Wiki.Url; } }
    }

    public class AnalyicsOverviewSection : BaseSection
    {
        public AnalyicsOverviewSection(RequestData requestData) : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var section = new TabSection("Overview", "Value");

            section.AddRow().Column("New vs. Returning").Column(RequestData[DataKey.IsNewVisitor]);
            section.AddRow().Column("Engagement Value").Column(RequestData[DataKey.EngagementValue]);
            section.AddRow().Column("Traffic Type").Column(RequestData[DataKey.TrafficType]);

            var campaign = RequestData[DataKey.Campaign];
            if (campaign != null) section.AddRow().Column("Campaign").Column(campaign);

            return section;
        }
    }
}