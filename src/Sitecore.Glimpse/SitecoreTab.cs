using System;

using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

using Sitecore.Glimpse.Infrastructure;

namespace Sitecore.Glimpse
{
    public class SitecoreTab : TabBase
    {
        private readonly ISitecoreRequest _sitecoreRequest;

        public SitecoreTab()
            : this(new SitecoreRequest())
        {
        }

        public SitecoreTab(ISitecoreRequest sitecoreRequest)
        {
            _sitecoreRequest = sitecoreRequest;
        }

        public override object GetData(ITabContext context)
        {
            try
            {
                var sitecoreData = _sitecoreRequest.GetData();

                if (! sitecoreData.HasData()) return null;

                var plugin = Plugin.Create("Sitecore Context Property", "Value");

                var itemSection = new ItemSection(sitecoreData).Create();
                var itemTemplateSection = new ItemTemplateSection(sitecoreData).Create();
                var itemVisualizationSection = new ItemVisualizationSection(sitecoreData).Create();
                var languageSection = new LanguageSection(sitecoreData).Create();
                var cultureSection = new CultureSection(sitecoreData).Create();
                var siteSection = new SiteSection(sitecoreData).Create();
                var databaseSection = new DatabaseSection(sitecoreData).Create();
                var deviceSection = new DeviceSection(sitecoreData).Create();
                var domainSection = new DomainSection(sitecoreData).Create();
                var diagnosticsSection = new DiagnosticsSection(sitecoreData).Create();
                var requestSection = new RequestSection(sitecoreData).Create();
                var userSection = new UserSection(sitecoreData).Create();

                if (itemSection != null)
                    plugin.Section("Item", itemSection);

                if (itemVisualizationSection != null)
                    plugin.Section("Item Visualization", itemVisualizationSection);

                if (itemTemplateSection != null)
                    plugin.Section("Item Template", itemTemplateSection);

                if (siteSection != null)
                    plugin.Section("Site", siteSection);
                
                if (databaseSection != null)
                    plugin.Section("Database", databaseSection);

                if (deviceSection != null)
                    plugin.Section("Device", deviceSection);

                if (domainSection != null)
                    plugin.Section("Domain", domainSection);

                if (diagnosticsSection != null)
                    plugin.Section("Diagnostics", diagnosticsSection);

                if (languageSection != null)
                    plugin.Section("Language", languageSection);

                if (cultureSection != null)
                    plugin.Section("Culture", cultureSection);

                if (requestSection != null)
                    plugin.Section("Request", requestSection);

                if (userSection != null)
                    plugin.Section("User", userSection);

                return plugin;
            }
            catch (Exception ex)
            {
                return new { Exception = ex };
            }
        }

        public override string Name
        {
            get { return "Sitecore"; }
        }
    }
}