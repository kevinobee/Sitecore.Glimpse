using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Analytics
{
    public class PageViewsSection
    {
        public static TabSection Create(RequestData requestData)
        {
            var lastPages = (PageHolder[]) requestData[DataKey.LastPages];

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