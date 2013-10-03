using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        public object GetField(string key)
        {
            if (_fields.Exists(x => x.Key.ToString(CultureInfo.InvariantCulture) == key))
            {
                return _fields.First(x => x.Key.ToString(CultureInfo.InvariantCulture) == key).Value;
            }
            
            return null;
        }
    }
}