using System;
using System.Linq;
using Moq;
using Should;
using Sitecore.Glimpse.Model;
using Xunit;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Test
{
    public class ControllersBehaviour
    {
        private readonly Controllers _sut;

        public ControllersBehaviour()
        {
            var types = new[]
            {
                typeof(System.Web.Mvc.Controller), 
                typeof(MvcController), 
                typeof(WebApiController),
                typeof(TestController),
                typeof(NonServicesTestController)
            }.AsQueryable();

            var servicesTypes = new[]
            {
                typeof(TestController),
                typeof(NonServicesTestController)
            }.AsQueryable(); 
            
            var typeProvider = new Mock<ITypeProvider>();
            var servicesTypeProvider = new Mock<ITypeProvider>();

            typeProvider
                .SetupGet(x => x.Types)
                .Returns(types);

            servicesTypeProvider
                .SetupGet(x => x.Types)
                .Returns(servicesTypes);

            _sut = new Controllers(typeProvider.Object, servicesTypeProvider.Object);
        }

        [Fact]
        public void collection_should_be_available()
        {
            _sut.Collection.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(typeof(System.Web.Mvc.Controller))]
        [InlineData(typeof(System.Web.Http.ApiController))]
        public void collection_should_not_contain_abstract_types(Type type)
        {
            _sut.Collection
                .Any(x => x.Name == type.FullName)
                .ShouldBeFalse();
        }

        [Theory]
        [InlineData(typeof(TestController))]
        [InlineData(typeof(NonServicesTestController))]
        public void collection_should_not_contain_sitecore_services_controllers(Type type)
        {
            _sut.Collection
                .Any(x => x.Name == type.FullName)
                .ShouldBeFalse();
        }

        [Theory]
        [InlineData(typeof(WebApiController), ControllerType.WebAPI)]
        [InlineData(typeof(MvcController), ControllerType.MVC)]
        public void controller_types_are_mapped_correctly_in_collection(Type type, ControllerType controllerType)
        {
            _sut.Collection
                .Single(x => x.Name == type.FullName)
                .ControllerType
                .ShouldEqual(controllerType);
        }
    }
}