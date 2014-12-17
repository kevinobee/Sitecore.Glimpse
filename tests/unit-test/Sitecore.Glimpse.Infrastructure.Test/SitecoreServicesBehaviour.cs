using System;
using Moq;
using Should;
using Sitecore.Services.Core;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test
{
    public class SitecoreServicesBehaviour
    {
        private readonly SitecoreServices _sut;
        private readonly Mock<IControllerNameGenerator> _nameGenerator;

        public SitecoreServicesBehaviour()
        {
            _nameGenerator = new Mock<IControllerNameGenerator>();

            _sut = new SitecoreServices(_nameGenerator.Object);
        }

        [Fact]
        public void should_return_services()
        {
            _sut.GetServices().ShouldNotBeNull();
        }

        [Fact(Skip = "TODO implement")]
        public void should_call_name_generator_to_set_route_for_services()
        {
            _sut.GetServices();

            _nameGenerator.Verify(x => x.GetName(It.IsAny<Type>()));
        }
    }
}