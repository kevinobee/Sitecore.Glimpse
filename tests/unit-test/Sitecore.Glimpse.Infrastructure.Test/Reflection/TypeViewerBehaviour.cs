using System;
using System.Linq;
using System.Web.Http;
using Should;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test.Reflection
{
    public class TypeViewerBehaviour
    {
        private readonly TypeViewer _sut;

        public TypeViewerBehaviour()
        {
            _sut = new TypeViewer(typeof (MyServicesApiController));
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

        [Fact]
        public void has_class_attribute_returns_false_by_default()
        {
            _sut.HasClassAttribute(typeof(ValidateHttpAntiForgeryToken).Name).ShouldBeFalse();
        }

        [Fact]
        public void has_class_attribute_returns_true_when_attribute_defined_on_class()
        {
            _sut.HasClassAttribute(typeof(MyCorsAttribute).Name).ShouldBeTrue();
        }

        [Fact]
        public void has_class_attribute_returns_true_when_attribute_defined_on_base_class()
        {
            _sut.HasClassAttribute(typeof(AnotherAttribute).Name).ShouldBeTrue();
        }

        [Fact]
        public void has_method_attribute_returns_false_by_default()
        {
            _sut.HasMethodAttribute(typeof(AnotherAttribute).Name)
                .ShouldBeFalse();
        }

        [Fact]
        public void has_method_attribute_returns_true_when_attribute_defined_on_a_class_method()
        {
            _sut.HasMethodAttribute(typeof(MyValidationAttribute).Name)
                .ShouldBeTrue();
        }

        [Fact]
        public void has_method_attribute_returns_true_when_attribute_defined_on_a_base_class_method()
        {
            _sut.HasMethodAttribute(typeof(AppliedToBaseClassMethodAttribute).Name)
                .ShouldBeTrue();
        }
    }

    public class ValidateHttpAntiForgeryToken : Attribute
    {
    }


    [MyCors("*", "*", "*")]
    public class MyServicesApiController : MyServicesBaseApiController<MyEntity>
    {
        [Obsolete]
        public string Get()
        {
            return null;
        }

        [MyValidation]
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

    public class MyValidationAttribute : Attribute
    {
    }

    [Another]
    public abstract class MyServicesBaseApiController<T> : ApiController
    {
        [AppliedToBaseClassMethodAttribute]
        public void InheritedPublicMethod()
        {
        } 
    }

    public class AnotherAttribute : Attribute
    {
    }


    public class AppliedToBaseClassMethodAttribute : Attribute
    {
    }

    public class MyEntity
    {
    }
}