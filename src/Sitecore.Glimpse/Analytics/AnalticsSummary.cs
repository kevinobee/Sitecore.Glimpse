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
            return "Insight" + GetProfileInsight();
        }

        private string GetProfileInsight()
        {
            var profiles = (Profile[]) _sitecoreData[DataKey.Profiles];

            if ((profiles == null) || (!profiles.Any(x => x.IsMatch))) return null;

            var matchingProfiles = profiles.Where(x => x.IsMatch).Select(x => new {x.Name, x.Dimension});

            var builder = new StringBuilder();
            foreach (var matchingProfile in matchingProfiles)
            {
                if (builder.Length == 0)
                {
                    builder.Append(" - ");
                }
                builder.AppendFormat("[{0} - {1}] ", matchingProfile.Dimension, matchingProfile.Name);
            }

            return builder.ToString();
        }
    }
}