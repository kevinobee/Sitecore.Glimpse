using System;
using System.Linq;
using System.Net.Http;
using Moq;
using Should;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Configuration;
using Sitecore.Services.Core.Model;
using Sitecore.Services.Infrastructure.Services;
using Sitecore.Services.Infrastructure.Web.Http;
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

            _typeProvider.SetupGet(x => x.Types).Returns(new[] { typeof(TestController), typeof(TestService) });

            _nameGenerator.Setup(x => x.GetName((It.IsAny<Type>()))).Returns("foo.bar");

            servicesConfiguration.SetupGet(x => x.Configuration)
                .Returns(new ServicesSettingsConfiguration
                {
                    Services =
                        new ServicesSettingsConfiguration.ServiceConfiguration()
                        {
                            Routes =
                                new ServicesSettingsConfiguration.ServiceConfiguration.RouteConfiguration()
                                {
                                    RouteBase = "/baseurl/"
                                }
                        }
                });
        }

        [Fact]
        public void should_return_services()
        {
            _sut.Collection.ShouldNotBeNull();
        }

        [Fact]
        public void should_handle_no_services_found()
        {
            _typeProvider.SetupGet(x => x.Types).Returns(new Type[]{});

            _sut.Collection.Count.ShouldEqual(0);
        }
        
        [Fact]
        public void non_entity_services_should_not_populate_metadata_or_objecttype_properties()
        {
            _typeProvider.SetupGet(x => x.Types).Returns(new[] { typeof(TestController) });

            var service = _sut.Collection.First();

            service.IsEntityService.ShouldBeFalse();
            service.Metadata.ShouldBeNull();
        }

        [Fact]
        public void calls_name_generator_to_set_url_for_services_when_ServicesController_attribute_is_on_controller()
        {
            var sitecoreServices = _sut.Collection;

            _nameGenerator.Verify(x => x.GetName(It.IsAny<Type>()));
        }

        [Fact]
        public void url_is_from_route_tab_for_services_when_ServicesController_attribute_is_not_on_controller()
        {
            _typeProvider.SetupGet(x => x.Types).Returns(new[] { typeof(NonServicesTestController) });

            var sitecoreServices = _sut.Collection;

            sitecoreServices.Single(x => x.Definition.Contains(typeof(NonServicesTestController).Name))
                            .Url
                            .ShouldEqual("See Routes tab for details");
        }

        [Fact]
        public void should_call_metadata_builder_to_set_metadata_for_services()
        {
            var sitecoreServices = _sut.Collection;

            _metadataBuilder.Verify(x => x.Parse(It.IsAny<Type>()));
        }
    }

    [ServicesController]
    public class TestController : ServicesApiController
    {
    }

    public class NonServicesTestController : ServicesApiController
    {
    }

    public class TestService : IEntityService<EntityIdentity>
    {
        public EntityIdentity[] FetchEntities()
        {
            throw new NotImplementedException();
        }

        public EntityIdentity FetchEntity(string id)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage CreateEntity(EntityIdentity entity)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage UpdateEntity(EntityIdentity entity)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Delete(EntityIdentity entity)
        {
            throw new NotImplementedException();
        }
    }
}