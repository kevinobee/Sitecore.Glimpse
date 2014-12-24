using System;
using Should;
using Sitecore.Glimpse.Caching;
using Sitecore.Glimpse.Extensions;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Core.Test.Extensions
{
    public class TypeExtensionsBehaviour
    {
        private readonly Type _sut;

        public TypeExtensionsBehaviour()
        {
            _sut = typeof(Derived);
        }

        [Theory]
        [InlineData("DerivedAttribute")]
        [InlineData("BaseAttribute")]
        public void gets_attributes_from_type(string attributeName)
        {
            _sut.GetAttributes().ShouldContain(attributeName);
        }

        [Theory]
        [InlineData(typeof(ICache), null)]
        [InlineData(typeof(ITestFeatures<>), typeof(ITestFeatures<string>))]
        public void gets_interface_from_type(Type interfaceType, Type expectedType)
        {
            _sut.GetGenericInterface(interfaceType).ShouldEqual(expectedType);
        }
    }

    [Derived]
    internal class Derived : Base
    { }

    [Base]
    internal class Base : ITestFeatures<string>
    { }

    internal interface ITestFeatures<T>
    {
    }

    internal class DerivedAttribute : Attribute
    { }

    internal class BaseAttribute : Attribute
    { }
}