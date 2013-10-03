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

            if ((profiles == null) || (!profiles.Any(x => x.IsMatch))) return null;
         
            var section = new TabSection();

            var maxProfiles = profiles.GroupBy(x => x.Dimension).Select(group => group.Count()).Max();

            var firstRow = section.AddRow().Column("Dimension");
            for (var i = 0; i < maxProfiles; i++)
            {
                firstRow.Column("");
            }

            foreach (var profileDimension in profiles.Select(x => x.Dimension).Distinct())
            {
                var row = section.AddRow().Column(profileDimension);
                var dimension = profileDimension;
                var pc = profiles.Where(x => x.Dimension == dimension).ToArray();
                
                for (var i = 0; i < maxProfiles; i++)
                {
                    if (i < pc.Count())
                    {
                        var profile = pc[i];
                        row.Column(profile.Name)
                            .UnderlineIf(profile.IsMatch);
                    }
                    else
                    {
                        row.Column("");
                    }
                }
            }

            return section;
        }
    }
}