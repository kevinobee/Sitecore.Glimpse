using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreAnalyticsForRequest : ISitecoreRequest
    {
        public RequestData GetData()
        {
            try
            {
                return GetAnalyticsData();
            }
            catch (InvalidOperationException exception)
            {
                if (! exception.Message.Contains("Could not get pipeline: loadVisitor")) throw;
            }
            
            return null;
        }

        private static RequestData GetAnalyticsData()
        {
            if (Tracker.CurrentVisit != null)
            {
                Tracker.Visitor.LoadAll();

                var data = new RequestData();

                data.Add(DataKey.Profiles, GetProfiles());
                data.Add(DataKey.Pattern, GetMatchingPattern());
                data.Add(DataKey.LastPages, GetLastPages(5));
                data.Add(DataKey.Goals, GetGoals(5));
                data.Add(DataKey.Campaign, GetCampaign());
                data.Add(DataKey.TrafficType, GetTrafficType());
                data.Add(DataKey.EngagementValue, GetEngagementValue());
                data.Add(DataKey.IsNewVisitor, GetVisitType());

                return data;
            }

            return null;
        }

        private static List<KeyValuePair<string, float>> GetProfiles()
        {
            // TODO reintroduce generic functionality

            //if (Tracker.CurrentVisit != null)
            //{
            //    var evaluatorTypeProfile = Context.Database.GetItem("95056025-5410-43DF-BB8C-67F8FEC8F45E");  // TODO const /sitecore/system/Marketing Center/Profiles/Product Interest - OfficeCore specific

            //    if (evaluatorTypeProfile != null)
            //    {
            //        if (Tracker.CurrentVisit.Profiles != null)
            //        {
            //            var results = new List<KeyValuePair<string, float>>();
                        
            //            var selected = from profile in Tracker.Visitor.DataSet.Profiles
            //                           where profile.ProfileName.Equals(evaluatorTypeProfile.Name, StringComparison.OrdinalIgnoreCase)
            //                           select profile;

            //            var groupedproffiles = selected.GroupBy(x => x.ProfileName);

            //            foreach (var profile in groupedproffiles)
            //            {
            //                var profileItem = new ProfileItem(Context.Database.GetItem("/sitecore/system/Marketing Center/Profiles/" + profile.Key));  // TODO const - OfficeCore specific
            //                foreach (var key in profileItem.Keys)
            //                {
            //                    var firstOrDefault = Tracker.CurrentVisit.Profiles.FirstOrDefault(x => x.ProfileName.Equals(profile.Key));
            //                    if (firstOrDefault != null)
            //                    {
            //                        results.Add(new KeyValuePair<string, float>(key.KeyName, firstOrDefault.Values[key.KeyName]));
            //                    }
            //                }
            //            }

            //            return results;
            //        }
            //    }
            //}

            return null;
        }

        private static Pattern GetMatchingPattern()
        {
            // TODO reintroduce generic functionality

//            if (Tracker.CurrentVisit != null)
//            {
//                var evaluatorTypeProfile = Context.Database.GetItem("95056025-5410-43DF-BB8C-67F8FEC8F45E");  // TODO const /sitecore/system/Marketing Center/Profiles/Product Interest - OfficeCore specific
//                // show the pattern match if there is one.
//
//                var personaProfile = Tracker.CurrentVisit.Profiles.FirstOrDefault(profile => profile.ProfileName == evaluatorTypeProfile.Name);
//                if (personaProfile != null)
//                {
//                    // load the details about the matching pattern
//                    var i = Context.Database.GetItem(new ID(personaProfile.PatternId));
//                    if (i != null)
//                    {
//                        return new Pattern(i.Fields["Name"].Value, 
//                                           i.Fields["Image"].Value,
//                                           i.Fields["Description"].Value);
//                    }
//                }
//            }

            return null;
        }

        private static Goal[] GetGoals(int numberOfGoals)
        {
            if (Tracker.CurrentVisit != null)
            {
                // TODO: Query the Sitecore Context rather than doing a join on the tables
                var pageEvents = Tracker.Visitor.DataContext.PageEvents
                    .Where(x => Context.Database.GetItem(new ID(x.PageEventDefinitionId)).Fields["IsGoal"].Value == "1")
                    .OrderByDescending(x => x.DateTime)
                    .Take(numberOfGoals)
                    .Select(x => new Goal() { Name = Context.Database.GetItem(new ID(x.PageEventDefinitionId)).Name, Timestamp = x.DateTime});

                return pageEvents.ToArray();

            }

            return null;
        }

        private static PageHolder[] GetLastPages(int numberOfPages)
        {
            var pages = Tracker.CurrentVisit.GetPages()
                                            .OrderByDescending(p => p.DateTime)
                                            .Skip(1)
                                            .Take(numberOfPages)
                                            .Select(x => new PageHolder(x.PageId, x.DateTime, x.Url)).ToArray();

            return pages;
        }

        private static string GetCampaign()  // TODO how do we demo a campaign with OfficeCore
        {
            if (!Tracker.CurrentVisit.IsCampaignIdNull())
            {
                var campaignId = Tracker.CurrentVisit.CampaignId.ToString();
                var campaign = Context.Database.GetItem(campaignId);
                return campaign.Name;
            }

            return null;
        }

        private static string GetTrafficType()
        {
            var trafficTypes = Context.Database.GetItem(SitecoreGlobals.Analytics.TrafficTypes);
            var items = trafficTypes.Axes.GetDescendants()
                                         .FirstOrDefault(p => p.Fields["Value"].Value == Tracker.CurrentVisit.TrafficType.ToString(CultureInfo.InvariantCulture));
            return items.Name;
        }

        private static string GetEngagementValue()
        {
            return Tracker.CurrentVisit.Value.ToString(CultureInfo.InvariantCulture);
        }

        private static string GetVisitType()
        {
            var visitCount = Tracker.Visitor.VisitCount;
            return visitCount > 1 ? "Returning" : "New";
        }
    }
}