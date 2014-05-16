using System.Collections.Generic;

using Sitecore.Glimpse.Model;
using Sitecore.Web.Authentication;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class CurrentUsers
    {
        public LoggedInUser[] GetUsers()
        {
            var sessions = DomainAccessGuard.Sessions;

            var users = new List<LoggedInUser>();

            foreach (var session in sessions)
            {
                var sitecoreUser = Security.Accounts.User.FromName(session.UserName, false);

                users.Add(new LoggedInUser(session.SessionID, session.UserName, session.Created, session.LastRequest, sitecoreUser.IsAdministrator));
            }

            return users.ToArray();
        }
    }
}