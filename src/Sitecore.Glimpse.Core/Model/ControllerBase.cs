namespace Sitecore.Glimpse.Model
{
    public abstract class ControllerBase
    {
        public string Name { get; set; }

        public Csrf CsrfProtection { get; set; }

        public bool Authorise { get; set; }

        public string Definition { get; set; }
    }
}