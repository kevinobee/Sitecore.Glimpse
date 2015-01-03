using System;

namespace Sitecore.Glimpse.Model.Analytics
{
    public class PageHolder
    {
        public int Num { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }

        public PageHolder(int num, Guid id, DateTime date, string url)
        {
            Num = num;
            Id = id;
            Date = date;
            Url = url;
        }
    }
}