namespace Sitecore.Glimpse.Model
{
    public class SitecoreService
    {
        public string ClassName { get; set; }
        public string Route { get; set; }
        public string[] Attributes { get; set; }

        public SitecoreService()
        {
            Attributes = new string[]{};
        }
    }
}