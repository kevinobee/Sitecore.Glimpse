using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Sitecore.Analytics;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreAnalyticsForRequest : ISitecoreRequest
    {
        private readonly ILog _logger;
        private readonly ISitecoreRepository _sitecoreRepository;

        public SitecoreAnalyticsForRequest(ILog logger, ISitecoreRepository sitecoreRepository)
        {
            _logger = logger;
            _sitecoreRepository = sitecoreRepository;
        }

        public SitecoreAnalyticsForRequest() 
            : this(new TraceLogger(), new CachingSitecoreRepository(new SitecoreRepository()))
        {           
        }

        public RequestData GetData()
        {
//            try
//            {
//                return GetAnalyticsData();
//            }
//            catch (Exception exception)
//            {
//                _logger.Write(string.Format("Failed to load Sitecore Analytics Glimpse data - {0}", exception.Message));
//            }

            return new RequestDataNotLoaded();
        }

//        private RequestData GetAnalyticsData()
//        {
//            if (Tracker.Current != null)
//            {
//                Tracker.Current.
//            }
//
//
//            if (Tracker.CurrentVisit != null)
//            {
//                Tracker.Visitor.LoadAll();
//
//                var data = new RequestData();
//
//                data.Add(DataKey.Profiles, GetProfiles());
//                data.Add(DataKey.LastPages, GetLastPages(5));
//                data.Add(DataKey.Goals, GetGoals(5));
//                data.Add(DataKey.Campaign, GetCampaign());
//                data.Add(DataKey.TrafficType, GetTrafficType());
//                data.Add(DataKey.EngagementValue, GetEngagementValue());
//                data.Add(DataKey.IsNewVisitor, GetVisitType());
//
//                return data;
//            }
//
//            return null;
//        }

//        private IEnumerable<Profile> GetProfiles()
//        {
//            var patternCards = _sitecoreRepository.GetPatternCards().ToArray();
// 
//            var patternMatched = GetAllPatternsMatched(patternCards).ToList();
//
//            var profiles =  patternCards.Select(x => new Profile
//                {
//                    Name = x.Name, 
//                    IsMatch = patternMatched.Any(m => m == x.ID),
//                    Dimension = x.Dimension
//                }).ToArray();
//
//            return profiles;
//        }
//
//        private IEnumerable<Guid> GetAllPatternsMatched(PatternCard[] patternCards)
//        {
//            var profileDimesions = patternCards.Select(x => x.Dimension).Distinct();
//
//            foreach (var profileDimension in profileDimesions)
//            {
//                var personaProfile = Tracker.CurrentVisit.Profiles.FirstOrDefault(profile => profile.ProfileName == profileDimension);
//
//                if (personaProfile != null)
//                {
//                    personaProfile.UpdatePattern();
//
//                    yield return patternCards.First(x => x.ID == personaProfile.PatternId).ID;
//                }
//            }
//        }
//
//        private Goal[] GetGoals(int numberOfGoals)
//        {
//            if (Tracker.CurrentVisit != null)
//            {
//                // TODO: Query the Sitecore Context rather than doing a join on the tables
//                var pageEvents = Tracker.Visitor.DataContext.PageEvents
//                    .Where(x => _sitecoreRepository.IsGoal(x.PageEventDefinitionId))
//                    .OrderByDescending(x => x.DateTime)
//                    .Take(numberOfGoals)
//                    .Select(x => new Goal
//                        {
//                            Name = _sitecoreRepository.GetItem(x.PageEventDefinitionId).Name, 
//                            Timestamp = x.DateTime
//                        });
//
//                return pageEvents.ToArray();
//            }
//
//            return null;
//        }
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