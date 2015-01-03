using System.Linq;

using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Analytics
{
    public class ProfilesSection : BaseSection
    {
        public ProfilesSection(RequestData requestData)
            : base(requestData)
        {            
        }

        public override TabSection Create()
        {
            var profiles = (Profile[]) RequestData[DataKey.Profiles];

            if ((profiles == null) || (profiles.Length == 0)) return null;
         
            var section = new TabSection();

            section.AddRow().Column("Profile name").Column("Pattern Matched").Column("Values");
            foreach (var profile in profiles)
            {
                section.AddRow()
                    .Column(profile.Name)
                    .Column(profile.PatternCard)
                    .Column(profile.Values);
            }

            return section;
        }
    }
}