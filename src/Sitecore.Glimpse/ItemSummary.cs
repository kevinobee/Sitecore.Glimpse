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
            var fieldList = GetKeyValue(DataKey.Item);

            if (fieldList == null) return null;

            var fullPath = fieldList.GetField("Full Path");
            var templateName = fieldList.GetField("Template Name");

            if (fullPath == null || templateName == null) return null;

            return string.Format("{0} [ {1} ]", fullPath, templateName);
        }

        private FieldList GetKeyValue(DataKey key)  // TODO refactor
        {
            return (FieldList)_sitecoreData.GetKeyValue(key);
        }
    }
}