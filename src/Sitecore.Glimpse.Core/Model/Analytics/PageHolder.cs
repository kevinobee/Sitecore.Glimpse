using System;

namespace Sitecore.Glimpse.Model.Analytics
{
    public class PageHolder
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }

        public PageHolder(Guid id, DateTime date, string url)
        {
            Id = id;
            Date = date;
            Url = url;
        }
    }
}