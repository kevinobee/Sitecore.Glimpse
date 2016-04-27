using System;
using Should;
using Sitecore.Glimpse.Caching;
using Sitecore.Glimpse.Infrastructure.Extensions;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Test.Extensions
{
    public class GetGenericInterfaceBehaviour
    {
        private readonly Type _sut;

        public GetGenericInterfaceBehaviour()
        {
            _sut = typeof(DerivedClass);
        }

        [Theory]
        [InlineData(typeof(ICache), null)]
        [InlineData(typeof(ITestFeatures<>), typeof(ITestFeatures<string>))]
        public void GetsInterfaceFromType(Type interfaceType, Type expectedType)
        {
            _sut.GetGenericInterface(interfaceType).ShouldEqual(expectedType);
        }
    }

    [Derived]
    internal class DerivedClass : BaseClass
    {
    }

    [Base]
    internal class BaseClass : ITestFeatures<string>
    {
        public void InheritedPublicMethod()
        {
        }
    }

    internal interface ITestFeatures<T>
    {
    }

    internal class DerivedAttribute : BaseAttribute
    {
    }

    internal class BaseAttribute : Attribute
    {
    }
}