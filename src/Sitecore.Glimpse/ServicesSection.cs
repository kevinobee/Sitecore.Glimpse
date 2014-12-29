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

            var section = new TabSection("Controller", "Url", "ES", "Definition", "Metadata");

            foreach (var service in services)
            {
                section.AddRow()
                    .Column(service.Controller)
                    .Column(service.Url)
                    .Column(service.IsEntityService ? "Yes" : "No")
                    .Column(service.Definition)
                    .Column(service.Metadata)
                    .WarnIf(service.CorsEnabled);
            }

            return section;
        }
    }
}