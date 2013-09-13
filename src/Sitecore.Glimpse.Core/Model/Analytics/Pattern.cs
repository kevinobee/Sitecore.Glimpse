namespace Sitecore.Glimpse.Model.Analytics
{
    public class Pattern
    {
        public string Name { get; private set; }
        public string Image { get; private set; }
        public string Description { get; private set; }

        public Pattern(string name, string image, string description)
        {
            Name = name;
            Image = image;
            Description = description;
        }
    }
}
