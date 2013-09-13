using System.Linq;

using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class RequestSection : BaseSection
    {
        const string QueryStringKey = "QueryString";

        public RequestSection(RequestData requestData)
            : base(requestData, DataKey.Request, "Request")
        {
        }

        public override TabSection Create()
        {
            var fieldList = (FieldList) RequestData[DataKey.Request];

            if (fieldList == null) return null;

            var section = new TabSection("Request Property", "Value");

            DisplayFields(fieldList.Fields.Where(x => x.Key != QueryStringKey).ToArray(), section);

            ParseQueryString(fieldList, section);

            return section;
        }

        private static void ParseQueryString(FieldList fieldList, TabSection section)
        {
            var queryStringFields = (FieldList) fieldList.Fields.SingleOrDefault(x => x.Key == QueryStringKey).Value;

            if (queryStringFields != null)
            {
                var queryStringSection = new TabSection("key", "value");

                DisplayFields(queryStringFields.Fields, queryStringSection);

                section.Section(QueryStringKey, queryStringSection);
            }
        }
    }
}