using System.Linq;
using Moq;
using Should;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Services.Infrastructure.Web.Http;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test.Reflection
{
    public class ServicesControllerAssemblyScannerBehaviour
    {
        private readonly ServicesControllerAssemblyScanner _sut;
        private readonly Mock<ITypeProvider> _typeProvider;

        public ServicesControllerAssemblyScannerBehaviour()
        {
            _typeProvider = new Mock<ITypeProvider>();

            _sut = new ServicesControllerAssemblyScanner(_typeProvider.Object);
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
            var types = new[]
            {
                typeof (object), 
                typeof (TestController), 
                typeof (NonServicesTestController)
            }.AsQueryable();

            _typeProvider.SetupGet(x => x.Types).Returns(types);

            _sut.Types
                .Any(x => ! typeof (ServicesApiController).IsAssignableFrom(x))
                .ShouldBeFalse();
        }
    }
}