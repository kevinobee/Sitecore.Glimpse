using System;

namespace Sitecore.Glimpse.Model
{
    public class LoggedInUser
    {
        public string SessionId { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastRequest { get; private set; }
        public bool IsAdmin { get; private set; }

        public LoggedInUser(string sessionId, string name, DateTime created, DateTime lastRequest, bool isAdmin)
        {
            SessionId = sessionId;
            Name = name;
            Created = created;
            LastRequest = lastRequest;
            IsAdmin = isAdmin;
        }

        public bool IsInactive()
        {
            return (SystemTime.Now.Invoke().Subtract(LastRequest).TotalHours >= 2);
        }
    }
}