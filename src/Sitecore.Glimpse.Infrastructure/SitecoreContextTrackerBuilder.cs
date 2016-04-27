namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreContextTrackerBuilder : ITrackerBuilder
    {
        public Analytics.ITracker Tracker
        {
            get { return Analytics.Tracker.Current; }
        }
    }
}
