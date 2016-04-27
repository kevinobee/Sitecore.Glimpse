using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Glimpse
{
    public class RequestData
    {
        private readonly Dictionary<DataKey, object> _requestData = new Dictionary<DataKey, object>();

        public object this[DataKey dataKey]
        {
            get
            {
                return GetKeyValue(dataKey);
            }

            set
            {
                _requestData[dataKey] = value;
            }
        }

        public void Add(DataKey dataKey, object value)
        {
            _requestData.Add(dataKey, value);
        }

        public bool HasData()
        {
            return 
                _requestData != null && 
                _requestData.Any(x => x.Value != null);
        }

        public object GetKeyValue(DataKey key)
        {
            return _requestData.ContainsKey(key) ? _requestData[key] : null;
        }
    }

    public class RequestDataNotLoaded : RequestData
    {
    }
}
