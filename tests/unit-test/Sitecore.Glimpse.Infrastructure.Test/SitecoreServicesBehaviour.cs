using System;
using System.Linq;
using Moq;
using Should;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Configuration;
using Sitecore.Services.Infrastructure.Services;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test
{
    public class SitecoreServicesBehaviour
    {
        private readonly SitecoreServices _sut;
        private readonly Mock<IControllerNameGenerator> _nameGenerator;
        private readonly Mock<ITypeProvider> _typeProvider;
        private readonly Mock<IMetaDataBuilder> _metadataBuilder;

        public SitecoreServicesBehaviour()
        {
            _typeProvider = new Mock<ITypeProvider>();
            _nameGenerator = new Mock<IControllerNameGenerator>();
            _metadataBuilder = new Mock<IMetaDataBuilder>();

            var servicesConfiguration = new Mock<IServicesConfiguration>();

            _sut = new SitecoreServices(
                _typeProvider.Object,
                _nameGenerator.Object,
                _metadataBuilder.Object,
                servicesConfiguration.Object);

            _typeProvider.SetupGet(x => x.Types)
                .Returns(new[] { typeof(TestController), typeof(TestService) }.AsQueryable);

            _nameGenerator.Setup(x => x.GetName(It.IsAny<Type>())).Returns("foo.bar");

            servicesConfiguration.SetupGet(x => x.Configuration)
                .Returns(
                    new ServicesSettingsConfiguration
                        {
                            Services =
                                new ServicesSettingsConfiguration.ServiceConfiguration
                                    {
                                        Routes =
                                            new ServicesSettingsConfiguration.ServiceConfiguration.RouteConfiguration
                                                {
                                                    RouteBase = "/baseurl/"
                                                }
                                    }
                        });
        }

        [Fact]
        public void ShouldReturnServices()
        {
            _sut.Collection.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldHandleNoServicesFound()
        {
            _typeProvider
                .SetupGet(x => x.Types)
                .Returns(new Type[] { }.AsQueryable);

            _sut.Collection
                .Count
                .ShouldEqual(0);
        }
        
        [Fact]
        public void NonEntityServicesShouldNotPopulateMetadataOrObjecttypeProperties()
        {
            _typeProvider
                .SetupGet(x => x.Types)
                .Returns(new[] { typeof(TestController) }.AsQueryable);

            var service = _sut.Collection.First();

            service.IsEntityService.ShouldBeFalse();
            service.Metadata.ShouldBeNull();
        }

        [Fact]
        public void CallsNameGeneratorToSetUrlForServicesWhenServicesControllerAttributeIsOnController()
        {
            var sitecoreServices = _sut.Collection;

            _nameGenerator.Verify(x => x.GetName(It.IsAny<Type>()));
        }

        [Fact]
        public void UrlIsFromRouteTabForServicesWhenServicesControllerAttributeIsNotOnController()
        {
            _typeProvider.SetupGet(x => x.Types).Returns(new[] { typeof(NonServicesTestController) }.AsQueryable);

            var sitecoreServices = _sut.Collection;

            sitecoreServices.Single(x => x.Definition.Contains(typeof(NonServicesTestController).Name))
                            .Url
                            .ShouldEqual("See Routes tab for details");
        }

        [Fact]
        public void ShouldCallMetadataBuilderToSetMetadataForServices()
        {
            var sitecoreServices = _sut.Collection;

            _metadataBuilder.Verify(x => x.Parse(It.IsAny<Type>()));
        }
    }
}