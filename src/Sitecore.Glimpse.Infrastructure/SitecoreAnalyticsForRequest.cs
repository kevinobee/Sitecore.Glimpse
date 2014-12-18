using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Sitecore;
using Sitecore.Glimpse.Model.Analytics;
using Sitecore.Analytics;
namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreAnalyticsForRequest : ISitecoreRequest
    {
        private readonly ILog _logger;
        private readonly ISitecoreRepository _sitecoreRepository;
        private readonly ITracker _tracker;

        public SitecoreAnalyticsForRequest(ILog logger, ISitecoreRepository sitecoreRepository,ITracker tracker)
        {
            _logger = logger;
            _sitecoreRepository = sitecoreRepository;
            _tracker = tracker;
        }

        public SitecoreAnalyticsForRequest() 
            : this(new TraceLogger(), new CachingSitecoreRepository(new SitecoreRepository()), Tracker.Current)
        {           
        }

        public RequestData GetData()
        {
            try
            {
                return GetAnalyticsData();
            }
            catch (Exception exception)
            {
                _logger.Write(string.Format("Failed to load Sitecore Analytics Glimpse data - {0}", exception.Message));
            }

            return new RequestDataNotLoaded();
        }

        private RequestData GetAnalyticsData()
        {
            if (_tracker.IsActive && _tracker.Interaction != null)
            {
                

                var data = new RequestData();

              //  data.Add(DataKey.Profiles, GetProfiles());
               // data.Add(DataKey.LastPages, GetLastPages(5));
                data.Add(DataKey.Goals, GetGoals(5));
              //  data.Add(DataKey.Campaign, GetCampaign());
               // data.Add(DataKey.TrafficType, GetTrafficType());
              //  data.Add(DataKey.EngagementValue, GetEngagementValue());
              //  data.Add(DataKey.IsNewVisitor, GetVisitType());

                return data;
            }

            return null;
        }

        //private IEnumerable<Profile> GetProfiles()
        //{
        //    var patternCards = _sitecoreRepository.GetPatternCards().ToArray();

        //    var patternMatched = GetAllPatternsMatched(patternCards).ToList();

        //    var profiles = patternCards.Select(x => new Profile
        //        {
        //            Name = x.Name,
        //            IsMatch = patternMatched.Any(m => m == x.ID),
        //            Dimension = x.Dimension
        //        }).ToArray();

        //    return profiles;

        //}

        //private IEnumerable<Guid> GetAllPatternsMatched(PatternCard[] patternCards)
        //{
        //    var profileDimesions = patternCards.Select(x => x.Dimension).Distinct();

        //    foreach (var profileDimension in profileDimesions)
        //    {
        //        var personaProfile = Tracker.CurrentVisit.Profiles.FirstOrDefault(profile => profile.ProfileName == profileDimension);

        //        if (personaProfile != null)
        //        {
        //            personaProfile.UpdatePattern();
        //            yield return patternCards.First(x => x.ID == personaProfile.PatternId).ID;
        //        }
        //    }
        //}
//
        private Goal[] GetGoals(int numberOfGoals)
        {
            if (_tracker.Interaction != null)
            {
                var pageNo = _tracker.Interaction.CurrentPage.VisitPageIndex;

                var goalList = new List<Goal>();

                while (goalList.Count < numberOfGoals && pageNo > 0)
                {
                    var page = _tracker.Interaction.GetPage(pageNo);
                    var goals = page.PageEvents.Select(ped=> new Goal(){Name = ped.Name, Timestamp=ped.DateTime});
                    goalList.AddRange(goals);
                }

                return goalList.Take(numberOfGoals).ToArray();
            }
            return null;
        }
//
//        private static PageHolder[] GetLastPages(int numberOfPages)
//        {
//            var pages = Tracker.CurrentVisit.GetPages()
//                                            .OrderByDescending(p => p.DateTime)
//                                            .Skip(1)
//                                            .Take(numberOfPages)
//                                            .Select(x => new PageHolder(x.PageId, x.DateTime, x.Url)).ToArray();
//
//            return pages;
//        }
//
//        private string GetCampaign()
//        {
//            if (!Tracker.CurrentVisit.IsCampaignIdNull())
//            {
//                var campaignId = Tracker.CurrentVisit.CampaignId.ToString();
//                var campaign = _sitecoreRepository.GetItem(campaignId);
//                return campaign.Name;
//            }
//
//            return null;
//        }
//
//        private string GetTrafficType()
//        {
//            var trafficTypes = _sitecoreRepository.GetItem(Constants.Sitecore.Analytics.Templates.TrafficTypes);
//            var items = trafficTypes.Axes.GetDescendants()
//                                         .FirstOrDefault(p => p.Fields["Value"].Value == Tracker.CurrentVisit.TrafficType.ToString(CultureInfo.InvariantCulture));
//            
//            return items != null ? items.Name : null;
//        }
//
//        private static string GetEngagementValue()
//        {
//            return Tracker.CurrentVisit.Value.ToString(CultureInfo.InvariantCulture);
//        }
//
//        private static string GetVisitType()
//        {
//            var visitCount = Tracker.Visitor.VisitCount;
//            return visitCount > 1 ? "Returning" : "New";
//        }
    }
}