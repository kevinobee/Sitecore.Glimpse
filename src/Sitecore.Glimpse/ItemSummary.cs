using System.Globalization;
using System.Linq;

namespace Sitecore.Glimpse
{
    public class ItemSummary
    {
        private readonly RequestData _sitecoreData;

        public ItemSummary(RequestData sitecoreData)
        {
            _sitecoreData = sitecoreData;
        }

        public string Create()
        {
            var fieldList = (FieldList)_sitecoreData[DataKey.Item];

            if (fieldList == null) return null;

            var fullPath = fieldList.Fields.FirstOrDefault(x => x.Key.ToString(CultureInfo.InvariantCulture) == "Full Path");
            var templateName = fieldList.Fields.FirstOrDefault(x => x.Key.ToString(CultureInfo.InvariantCulture) == "Template Name");

            return string.Format("{0} [ {1} ]", fullPath.Value, templateName.Value);
        }
    }
}