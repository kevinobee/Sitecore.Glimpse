using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public abstract class BaseSection
    {
        protected readonly RequestData RequestData;
        private readonly DataKey _dataKey;
        private readonly string _sectionName;

        protected BaseSection(RequestData requestData, DataKey dataKey, string sectionName)
        {
            RequestData = requestData;
            _dataKey = dataKey;
            _sectionName = sectionName;
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
            var fieldList = (FieldList)RequestData[_dataKey];

            if (fieldList == null) return null;

            var section = new TabSection(_sectionName + " Property", "Value");

            DisplayFields(fieldList.Fields, section);

            return section;
        }
    }
}