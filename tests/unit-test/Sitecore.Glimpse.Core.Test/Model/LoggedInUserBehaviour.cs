using System;

using Sitecore.Glimpse.Model;

using Should;
using Xunit;

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

        [Fact]
        public void should_show_user_to_be_active_if_last_request_less_than_two_hours_old()
        {
            var user = new LoggedInUser("sessionId", "name", DateTime.Now.AddHours(-3), _referenceTime.AddMinutes(-119), false);

            user.IsInactive().ShouldBeFalse();
        }

        [Fact]
        public void should_show_user_to_be_inactive_if_last_request_two_hours_or_more_old()
        {
            var user = new LoggedInUser("sessionId", "name", DateTime.Now.AddHours(-3), _referenceTime.AddMinutes(-120), false);

            user.IsInactive().ShouldBeTrue();
        }
    }
}
