using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreContextTrackerBuilder : ITrackerBuilder
    {
        public Analytics.ITracker Tracker
        {
            get { return Sitecore.Analytics.Tracker.Current; }
        }
    }
}
