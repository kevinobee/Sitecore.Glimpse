using System.Web.Http;
using Should;
using Sitecore.Glimpse.Infrastructure.Extensions;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Glimpse.Infrastructure.Test.Reflection;
using Sitecore.Glimpse.Model;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test.Extensions
{
    public class TypeViewerExtensionsBehaviour
    {
        [Fact]
        public void check_for_authorise_finds_derived_authorization_attributes()
        {
            var controllerViewer = new TypeViewer(typeof(AnalyticsDataController));

            controllerViewer.CheckForAuthorise(ControllerType.WebAPI)
                .ShouldBeTrue();
        }
    }

    public class AnalyticsDataController : SomeBaseController
    {
    }

    [SomeAuthorization]
    public class SomeBaseController : ApiController
    {
    }

    public class SomeAuthorizationAttribute : AuthorizeAttribute
    {
    }
}