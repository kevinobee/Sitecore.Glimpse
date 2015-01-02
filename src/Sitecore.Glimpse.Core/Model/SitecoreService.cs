namespace Sitecore.Glimpse.Model
{
    public class SitecoreService
    {
        public string Controller { get; set; }
        public string Url { get; set; }
        public string Metadata { get; set; }

        public bool IsEntityService { get; set; }

        public bool CorsEnabled
        {
            get { return (!string.IsNullOrEmpty(Definition)) && Definition.Contains("EnableCors"); }
        }

        public string Definition { get; set; }
    }
}