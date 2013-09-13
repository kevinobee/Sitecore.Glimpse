using System.Collections.Generic;

namespace Sitecore.Glimpse
{
    public class FieldList 
    {
        private readonly List<KeyValuePair<string, object>> _fields;

        public FieldList()
        {
            _fields = new List<KeyValuePair<string, object>>();
        }

        public KeyValuePair<string, object>[] Fields { get { return _fields.ToArray(); } }

        public void AddField(string key, object value)
        {
            _fields.Add(new KeyValuePair<string, object>(key,value));
        }
    }
}