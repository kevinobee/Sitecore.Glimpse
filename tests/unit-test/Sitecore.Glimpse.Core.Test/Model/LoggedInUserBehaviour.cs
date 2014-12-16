using System;

using Sitecore.Glimpse.Model;

using Should;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Core.Test.Model
{
    public class LoggedInUserBehaviour
    {
        private DateTime _referenceTime;

        public LoggedInUserBehaviour()
        {
            _referenceTime = DateTime.Now.AddHours(-2);
            SystemTime.Now = () => _referenceTime;
        }

        [Theory]
        [InlineData(-119, false)]
        [InlineData(-120, true)]
        public void user_active_reporting(double minutes, bool isInactive)
        {
            var user = new LoggedInUser("sessionId", "name", DateTime.Now.AddHours(-3), _referenceTime.AddMinutes(minutes), false);

            user.IsInactive().ShouldEqual(isInactive);
        }
    }
}