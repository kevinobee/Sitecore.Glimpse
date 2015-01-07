using System.Linq;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse
{
    public class ServicesSection : BaseSection
    {
        public ServicesSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var services = (SitecoreService[])RequestData[DataKey.Services];

            if ((services == null) || (!services.Any())) return null;

            var section = new TabSection("Controller", "Url", "ES", "Authorise", "CSRF Protection", "Definition", "Metadata");

            foreach (var service in services)
            {
                section.AddRow()
                    .Column(service.Name)
                    .Column(service.Url)
                    .Column(service.IsEntityService ? "Yes" : "No")
                    .Column(service.Authorise ? "Yes" : "No")
                    .Column(service.CsrfProtection.ToString())
                    .Column(service.Definition)
                    .Column(service.Metadata)
                    .WarnIf(service.CorsEnabled);
            }

            return section;
        }
    }
}