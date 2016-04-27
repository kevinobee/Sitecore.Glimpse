using Sitecore.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    public interface ITrackerBuilder
    {
        ITracker Tracker { get; }
    }
}