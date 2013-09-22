namespace Sitecore.Glimpse.Analytics
{
    public class AnalticsSummary
    {
        private readonly RequestData _sitecoreData;

        public AnalticsSummary(RequestData sitecoreData)
        {
            _sitecoreData = sitecoreData;
        }

        public string Create()
        {
//            var fieldList = (FieldList)_sitecoreData[DataKey.Item];
//
//            if (fieldList == null) return null;
//
//            var fullPath = fieldList.Fields.FirstOrDefault(x => x.Key.ToString(CultureInfo.InvariantCulture) == "Full Path");
//            var templateName = fieldList.Fields.FirstOrDefault(x => x.Key.ToString(CultureInfo.InvariantCulture) == "Template Name");

            return "Insight";

//            return string.Format("{0} [ {1} ]", fullPath.Value, templateName.Value);

        }
    }
}