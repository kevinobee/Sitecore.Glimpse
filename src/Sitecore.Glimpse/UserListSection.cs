using System.Linq;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse
{
    public class UserListSection : BaseSection
    {
        public UserListSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var users = (LoggedInUser[])RequestData[DataKey.UserList];

            if ((users == null) || (!users.Any())) return null;

            var section = new TabSection("Username", "Session ID", "Admin", "Created", "Last Request");

            foreach (var user in users)
            {
                var row = section.AddRow()
                  .Column(user.Name)
                  .Column(user.SessionId)
                  .Column(user.IsAdmin ? "Yes" : "No")
                  .Column(user.Created)
                  .Column(user.LastRequest);

                if (user.IsInactive())
                {
                    row.ApplyRowStyle("warn");
                }
            }

            return section;
        }
    }
}