using System.Collections.Generic;
using System.Linq;
using Sitecore.Glimpse.Model;
using Sitecore.Web.Authentication;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class CurrentUsers : ICollectionProvider<LoggedInUser>
    {
        public ICollection<LoggedInUser> Collection
        {
            get { return DomainAccessGuard.Sessions.Select(GetSitecoreUser).ToArray(); }
        }

        private static LoggedInUser GetSitecoreUser(DomainAccessGuard.Session session)
        {
            var sitecoreUser = Security.Accounts.User.FromName(session.UserName, false);

            return new LoggedInUser(
                            session.SessionID, 
                            session.UserName, 
                            session.Created, 
                            session.LastRequest,
                            sitecoreUser.IsAdministrator);
        }
    }
}