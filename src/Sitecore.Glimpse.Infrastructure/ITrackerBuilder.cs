using Sitecore.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Glimpse.Infrastructure
{
    public interface ITrackerBuilder
    {
        ITracker Tracker { get; }
    }
}
