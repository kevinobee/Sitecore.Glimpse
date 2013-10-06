using System;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

using Sitecore.Glimpse.Infrastructure;

namespace Sitecore.Glimpse
{
    public class SitecoreTab : TabBase, IDocumentation
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

                if (!sitecoreData.HasData()) return null;

                var itemSummary = new ItemSummary(sitecoreData).Create();

                if (string.IsNullOrEmpty(itemSummary)) return null;

                var plugin = Plugin.Create("Item", itemSummary);

                var itemSection = new ItemSection(sitecoreData).Create();
                var contextSection = new ContextSection(sitecoreData).Create();

                if (itemSection != null)
                    plugin.AddRow().Column("Item").Column(itemSection).Selected();

                if (contextSection != null)
                    plugin.AddRow().Column("Context").Column(contextSection).Quiet();

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

        public string DocumentationUri { get { return Constants.Wiki.Url; } }
    }
}