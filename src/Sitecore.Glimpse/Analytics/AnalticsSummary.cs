using System.Linq;

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
            const string DefaultInsight = "Insight";

            var profiles = (Profile[])_sitecoreData[DataKey.Profiles];

            if ((profiles == null) || (profiles.Length == 0))
            {
                return DefaultInsight;
            }

            var matchedProfiles = profiles.Where(p => !string.IsNullOrEmpty(p.PatternCard)).ToArray();

            if (!matchedProfiles.Any())
            {
                return DefaultInsight;
            }

            return 
                matchedProfiles
                    .Select(p => p.PatternCard)
                    .Aggregate((acu, ele) => acu += ", " + ele);
        }
    }
}