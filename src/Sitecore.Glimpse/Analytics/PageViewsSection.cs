using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Analytics
{
    public class PageViewsSection
    {
        public static TabSection Create(IDictionary<string, object> sitecoreData)
        {
            var lastPages = (PageHolder[]) sitecoreData[DataKey.LastPages];

            if ((lastPages == null) || (lastPages.Length == 0)) return null;

            var section = new TabSection("Id", "Timestamp", "Url");

            foreach (var pageHolder in lastPages)
            {
                section.AddRow().Column(pageHolder.Id).Column(pageHolder.Date).Column(pageHolder.Url);
            }

            return section;
        }
    }
}