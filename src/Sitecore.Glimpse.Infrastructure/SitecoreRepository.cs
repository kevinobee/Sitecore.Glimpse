using System;
using System.Linq;

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreRepository : ISitecoreRepository
    {
        public Item GetItem(string itemId)
        {
            return Context.Database.GetItem(new ID(itemId));
        }

        public Item GetItem(Guid itemId)
        {
            return Context.Database.GetItem(new ID(itemId));
        }

        public PatternCard[] GetPatternCards()
        {
            var profileItem = GetItem(Constants.Sitecore.MarketingCenter.Profiles);

            var selectItems = profileItem.Axes
                                         .SelectItems(
                                            string.Format(
                                                ".//*[@@templateid = '{0}']",
                                                Constants.Sitecore.Analytics.Templates.PatternCard));

            if (selectItems != null)
            {
                return selectItems.Select(x => new PatternCard
                    {
                        ID = x.ID.Guid, 
                        Name = x.Name, 
                        Dimension = GetProfileDimension(x)
                    }).ToArray();
            }
            
            return new PatternCard[] { };
        }

        public bool IsGoal(Guid pageEventDefinitionId)
        {
            return GetItem(pageEventDefinitionId).Fields["IsGoal"].Value == "1";
        }

        private static string GetProfileDimension(Item item)
        {
            while (true)
            {
                if (item.TemplateID == new ID(Constants.Sitecore.Analytics.Templates.Profile))
                {
                    return item.Name;
                }

                if (item.Parent == null)
                {
                    return null;
                }

                item = item.Parent;
            }
        }
    }
}