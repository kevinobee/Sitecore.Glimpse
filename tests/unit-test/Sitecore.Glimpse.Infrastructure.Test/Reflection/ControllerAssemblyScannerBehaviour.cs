using System.Linq;
using Should;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test.Reflection
{
    public class ControllerAssemblyScannerBehaviour
    {
        private readonly ControllerAssemblyScanner _sut;

        public ControllerAssemblyScannerBehaviour()
        {
            _sut = new ControllerAssemblyScanner();
        }

        [Fact]
        public void implements_type_provider_interface()
        {
            _sut.ShouldImplement<ITypeProvider>();
        }

        [Fact]
        public void returns_controllers()
        {
            _sut.Types.ShouldNotBeNull();
        }

        [Fact]
        public void all_controllers_returned_are_sitecore_service_controllers()
        {
            _sut.Types.Any(x => ! typeof (ServicesApiController).IsAssignableFrom(x)).ShouldBeFalse();
        }
    }
}