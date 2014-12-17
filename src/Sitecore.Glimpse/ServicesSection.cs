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

            var section = new TabSection("Class Name", "Route", "Attributes");

            foreach (var service in services)
            {
                var row = section.AddRow()
                    .Column(service.ClassName)
                    .Column(service.Route)
                    .Column(service.Attributes)
//                  .Column(service.IsAdmin ? "Yes" : "No")
//                  .Column(service.Created)
//                  .Column(service.LastRequest);
                    ;
//                if (service.IsInactive())
//                {
//                    row.ApplyRowStyle("warn");
//                }
            }

            return section;

        }
    }
}