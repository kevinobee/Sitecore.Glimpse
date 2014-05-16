using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class LicenseSection : BaseSection
    {
        public LicenseSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var fields = (FieldList)RequestData[DataKey.License];

            if ((fields == null) || (fields.Fields.Length == 0)) return null;

            var section = new TabSection("Property", "Value");

            foreach (var field in fields.Fields)
            {
                section.AddRow().Column(field.Key).Column(field.Value);
            }

            return section;
        }
    }
}