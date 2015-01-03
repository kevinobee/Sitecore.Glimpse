using System;
using System.Linq;
using Sitecore.Glimpse.Reflection;
using Should;
using Xunit;

namespace Sitecore.Glimpse.Core.Test.Reflection
{
    public class TypeViewerBehaviour
    {
        private readonly TypeViewer _sut;

        public TypeViewerBehaviour()
        {
            _sut = new TypeViewer(typeof (MyServicesApiController), IsRootType, IsRootAttribute);
        }

        private bool IsRootType(Type type)
        {
            return type == null || type.BaseType == null;
        }

        private bool IsRootAttribute(Type type)
        {
            return type == null || type.BaseType == null;
        }

        [Fact]
        public void should_return_public_methods()
        {
            _sut.Methods.Count(x => x.Name == "Get").ShouldEqual(1);
        }

        [Fact]
        public void should_not_return_protected_methods()
        {
            _sut.Methods.Count(x => x.Name == "ProtectedMethod").ShouldEqual(0);
        }

        [Fact]
        public void should_not_return_private_methods()
        {
            _sut.Methods.Count(x => x.Name == "PrivateMethod").ShouldEqual(0);
        }

        [Fact]
        public void should_not_return_internal_methods()
        {
            _sut.Methods.Count(x => x.Name == "InternalMethod").ShouldEqual(0);
        }

        [Fact]
        public void should_return_inherited_public_methods_via_base()
        {
            _sut.Base.Methods.Count(x => x.Name == "InheritedPublicMethod").ShouldEqual(1);
        }

        [Fact]
        public void shows_controller_base_class()
        {
            _sut.Base.Name.ShouldEqual(typeof(MyServicesBaseApiController<MyEntity>).FullName);
        }

        [Fact]
        public void ToJson_returns_data()
        {
            _sut.ToJson().ShouldNotBeNull();
        }

        [Fact]
        public void json_does_not_contain_base_null()
        {
            _sut.ToJson().ShouldNotContain("\"Base\": null");
        }

        [Fact]
        public void json_does_not_contain_attributes_empty_array()
        {
            _sut.ToJson().ShouldNotContain("\"Attributes\": []");
        }

        [Fact]
        public void json_does_not_contain_methods_empty_array()
        {
            _sut.ToJson().ShouldNotContain("\"Methods\": []");
        }

        [Fact]
        public void json_contains_my_cors_attribute()
        {
            _sut.ToJson().ShouldContain("MyCors");
        }
    }

    [MyCors("*", "*", "*")]
    public class MyServicesApiController : MyServicesBaseApiController<MyEntity>
    {
        [Obsolete]
        public string Get()
        {
            return null;
        }

        public bool GetFoo()
        {
            return true;
        }

        protected void ProtectedMethod()
        {
        }

        private void PrivateMethod()
        {
        }

        internal void InternalMethod()
        {
        }
    }

    public class MyCorsAttribute : Attribute
    {
        public MyCorsAttribute(string s, string s1, string s2)
        {
        }
    }

    public class DenyAnonymousUserAttribute : Attribute
    {
    }

    public abstract class MyServicesBaseApiController<T> 
    {
        public void InheritedPublicMethod()
        {
        } 
    }

    public class MyEntity
    {
    }
}