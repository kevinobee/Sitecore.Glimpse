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

            if ((users == null) || (!users.Any()))
            {
                return null;
            }

            var section = new TabSection("Username", "Session ID", "Admin", "Created", "Last Request");

            foreach (var row in
                users.Select(user => new 
                        {
                            user,
                            row = section.AddRow()
                                         .Column(user.Name)
                                         .Column(user.SessionId)
                                         .Column(user.IsAdmin ? "Yes" : "No")
                                         .Column(user.Created)
                                         .Column(user.LastRequest)
                        })
                     .Where(@t => @t.user.IsInactive())
                     .Select(@t => @t.row))
            {
                row.ApplyRowStyle("warn");
            }

            return section;
        }
    }
}