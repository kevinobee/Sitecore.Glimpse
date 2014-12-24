using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Analytics
{
    public class PageViewsSection : BaseSection
    {
        public PageViewsSection(RequestData requestData) : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var lastPages = (PageHolder[]) RequestData[DataKey.LastPages];

            if ((lastPages == null) || (lastPages.Length == 0)) return null;

            var section = new TabSection("#","Id", "Timestamp", "Url");

            foreach (var pageHolder in lastPages)
            {
                section.AddRow().Column(pageHolder.Num).Column(pageHolder.Id).Column(pageHolder.Date).Column(pageHolder.Url);
            }

            return section;
        }
    }
}