using System.Linq;
using System.Text;
using Sitecore.Glimpse.Model.Analytics;

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
            const string defaultInsight = "Insight";

            var profiles = (Profile[]) _sitecoreData[DataKey.Profiles];

            if ((profiles == null) || (profiles.Length == 0)) return defaultInsight;

            var matchedProfiles = profiles.Where(p => !string.IsNullOrEmpty(p.PatternCard));
            
            if(!matchedProfiles.Any()) return defaultInsight;
            
            return matchedProfiles.Select(p => p.PatternCard)
                .Aggregate((acu, ele) => acu += ", "+ele );
            
        }
    }
}