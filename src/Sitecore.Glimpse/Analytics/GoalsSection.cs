using System.Collections.Generic;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Analytics
{
    public class GoalsSection
    {
        public static TabSection Create(IDictionary<string, object> sitecoreData)
        {
            var goals = (Goal[]) sitecoreData[DataKey.Goals];

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