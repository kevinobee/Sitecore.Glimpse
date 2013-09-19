using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public abstract class BaseSection
    {
        protected readonly RequestData RequestData;
        private readonly DataKey _dataKey;

        protected BaseSection(RequestData requestData, DataKey dataKey)
        {
            RequestData = requestData;
            _dataKey = dataKey;
        }

        protected BaseSection()
        {
        }

        protected static void DisplayFields(KeyValuePair<string, object>[] fields, TabSection section)
        {
            foreach (var field in fields)
            {
                section.AddRow().Column(field.Key).Column(field.Value);
            }
        }

        public virtual TabSection Create()
        {
            return CreateSection(RequestData, _dataKey);
        }

        protected TabSection CreateSection(RequestData requestData, DataKey dataKey)
        {
            var fieldList = (FieldList)requestData[dataKey];

            if (fieldList == null) return null;

            var section = new TabSection("Property", "Value");

            DisplayFields(fieldList.Fields, section);

            return section;            
        }
    }
}