using System.Linq;
using Glimpse.Core.Tab.Assist;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse
{
    public class ControllersSection : BaseSection
    {
        public ControllersSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var controllers = (Controller[])RequestData[DataKey.Controllers];

            if ((controllers == null) || (!controllers.Any())) return null;

            var section = new TabSection("Controller", "Type", "Authorise", "CSRF Protection", "Definition");

            foreach (var controller in controllers)
            {
                section.AddRow()
                    .Column(controller.Name)
                    .Column(controller.ControllerType.ToString())
                    .Column(controller.Authorise ? "Yes" : "No")
                    .Column(controller.CsrfProtection.ToString())
                    .Column(controller.Definition);
            }

            return section;
        }
    }
}