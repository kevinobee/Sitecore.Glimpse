using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetUserPropertiesFull(Sitecore.Security.Accounts.User u)
        {
            var results = new List<object[]>()
                {
                    new object[] { "User Property", "Value" },
                    new object[] { "Name", u.Name},
                    new object[] { "DisplayName", u.DisplayName},
                    new object[] { "Roles", u.Roles.Select(r => r.Name)},
                    // new object[] { "AccountType", u.AccountType},
                    new object[] { "Description", u.Description},
                    new object[] { "Domain Name", u.GetDomainName()},
                    new object[] { "IsAdministrator", u.IsAdministrator},
                    new object[] { "IsAuthenticated", u.IsAuthenticated},
                    new object[] { "LocalName", u.LocalName},
                    // new object[] { "Profile", u.Profile},
                };
            return results;
        }

    }
}
