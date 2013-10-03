using Glimpse.Core.Tab.Assist;

using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Analytics
{
    public class GoalsSection : BaseSection
    {
        public GoalsSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var goals = (Goal[]) RequestData[DataKey.Goals];

            if ((goals == null) || (goals.Length == 0)) return null; 
            
            var section = new TabSection("Timestamp", "Name");

            foreach (var goal in goals)
            {
                section.AddRow().Column(goal.Timestamp).Column(goal.Name);
            }

            return section;
        }
    }
}