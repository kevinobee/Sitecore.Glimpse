using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetDomainPropertiesFull(Sitecore.Security.Domains.Domain d)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Domain Property", "Value" },
                    new object[] { "Name", d.Name},
                    new object[] { "Is Default", d.IsDefault},
                    new object[] { "Account Prefix", d.AccountPrefix},
                    new object[] { "Anonymous User Name", d.AnonymousUserName},
                    new object[] { "Default Profile Item ID", d.DefaultProfileItemID},
                    new object[] { "Ensure Anonymous User", d.EnsureAnonymousUser},
                    new object[] { "EveryoneR ole Name", d.EveryoneRoleName},
                    new object[] { "Locally Managed", d.LocallyManaged},
                    new object[] { "Anonymous User Email Pattern", d.AnonymousUserEmailPattern},
                    new object[] { "Account Name Validation", d.AccountNameValidation},
                    new object[] { "Member Pattern", d.MemberPattern}
                };
            return results;
        }

    }
}
