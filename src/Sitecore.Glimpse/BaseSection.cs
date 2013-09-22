using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public abstract class BaseSection
    {
        protected readonly RequestData RequestData;

        protected BaseSection(RequestData requestData)
        {
            RequestData = requestData;
        }

        public abstract TabSection Create();

        protected static void DisplayFields(KeyValuePair<string, object>[] fields, TabSection section)
        {
            foreach (var field in fields)
            {
                section.AddRow().Column(field.Key).Column(field.Value);
            }
        }

        protected TabSection CreateSection(DataKey dataKey)
        {
            var fieldList = (FieldList)RequestData[dataKey];

            if (fieldList == null) return null;

            var section = new TabSection("Property", "Value");

            DisplayFields(fieldList.Fields, section);

            return section;            
        }
    }
}