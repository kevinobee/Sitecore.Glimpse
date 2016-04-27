using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreAnalyticsForRequest : ISitecoreRequest
    {
        private readonly ILog _logger;
        private readonly ISitecoreRepository _sitecoreRepository;
        private readonly ITrackerBuilder _trackerBuilder;

        public SitecoreAnalyticsForRequest(ILog logger, ISitecoreRepository sitecoreRepository, ITrackerBuilder trackerBuilder)
        {
            _logger = logger;
            _sitecoreRepository = sitecoreRepository;
            _trackerBuilder = trackerBuilder;
        }

        public SitecoreAnalyticsForRequest() 
            : this(new TraceLogger(), new CachingSitecoreRepository(new SitecoreRepository()), new SitecoreContextTrackerBuilder())
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
            var tracker = _trackerBuilder.Tracker;

            if (tracker.Interaction == null)
            {
                return null;
            }

            var data = new RequestData();

            data.Add(DataKey.Profiles, GetProfiles());
            data.Add(DataKey.LastPages, GetLastPages(5));
            data.Add(DataKey.Goals, GetGoals(5));
            data.Add(DataKey.Campaign, GetCampaign());
            data.Add(DataKey.TrafficType, GetTrafficType());
            data.Add(DataKey.EngagementValue, GetEngagementValue());
            data.Add(DataKey.IsNewVisitor, GetVisitType());

            return data;
        }

        private IEnumerable<Profile> GetProfiles()
        {
            var analyticsProfiles = _trackerBuilder.Tracker.Interaction.Profiles;

            return
                analyticsProfiles
                    .GetProfileNames()
                    .Select(profileName => new { profileName, profile = analyticsProfiles[profileName] })
                    .Select(
                        @t =>
                        new Profile
                            {
                                Name = @t.profileName,
                                PatternCard = @t.profile.PatternLabel,
                                Values = @t.profile.ToString()
                            });
        }

        private IEnumerable<Guid?> GetMatchedPatternCards()
        {
            var profileNames = _trackerBuilder.Tracker.Interaction.Profiles.GetProfileNames();

            return 
                profileNames.Select(profileName => _trackerBuilder.Tracker.Interaction.Profiles[profileName].PatternId);
        }

        private Goal[] GetGoals(int numberOfGoals)
        {
            var tracker = _trackerBuilder.Tracker;

            if (tracker.Interaction == null)
            {
                return null;
            }

            var pageNo = tracker.Interaction.CurrentPage.VisitPageIndex;

            var goalList = new List<Goal>();

            while (goalList.Count < numberOfGoals && pageNo > 0)
            {
                var page = tracker.Interaction.GetPage(pageNo);
                var goals = page.PageEvents
                                .Select(ped => new Goal
                                                   {
                                                       Name = ped.Name, 
                                                       Timestamp = ped.DateTime
                                                   });

                goalList.AddRange(goals);
                pageNo--;
            }

            return goalList.Take(numberOfGoals).ToArray();
        }

        private PageHolder[] GetLastPages(int numberOfPages)
        {
            return
                _trackerBuilder.Tracker
                               .Interaction
                               .GetPages()
                               .OrderByDescending(p => p.DateTime)
                               .Skip(1)
                               .Take(numberOfPages)
                               .Select(x => new PageHolder(
                                                    x.VisitPageIndex, 
                                                    x.Item.Id, 
                                                    x.DateTime, 
                                                    x.Url.ToString()))
                               .ToArray();
        }

        private string GetCampaign()
        {
            var campaignId = _trackerBuilder.Tracker.Interaction.CampaignId;

            if (campaignId == null)
            {
                return null;
            }

            var campaign = _sitecoreRepository.GetItem(campaignId.ToString());
                
            return 
                campaign != null 
                    ? campaign.Name 
                    : campaignId.ToString();
        }

        private string GetTrafficType()
        {
            // TODO get through analytics items
            var trafficTypes = _sitecoreRepository.GetItem(Constants.Sitecore.Analytics.Templates.TrafficTypes);
            var items = trafficTypes.Axes.GetDescendants()
                                         .FirstOrDefault(p => p.Fields["Value"].Value == _trackerBuilder.Tracker.Interaction.TrafficType.ToString(CultureInfo.InvariantCulture));

            return items != null ? items.Name : null;
        }

        private string GetEngagementValue()
        {
            return _trackerBuilder.Tracker.Interaction.Value.ToString(CultureInfo.InvariantCulture);
        }

        private string GetVisitType()
        {
            var visitCount = _trackerBuilder.Tracker.Interaction.ContactVisitIndex;
            return visitCount > 1 ? "returning" : "new";
        }
    }
}
