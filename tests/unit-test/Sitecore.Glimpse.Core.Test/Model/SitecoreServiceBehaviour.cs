using Ploeh.AutoFixture.Xunit;
using Should;
using Sitecore.Glimpse.Model;
using Xunit;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Core.Test.Model
{
    public class SitecoreServiceBehaviour
    {
        private readonly SitecoreService _sut;

        public SitecoreServiceBehaviour()
        {
            _sut = new SitecoreService();            
        }

        [Theory]
        [InlineAutoData(false)]
        [InlineData(true, new[] { "SomeAttribute", "EnableCorsAttribute" })]
        public void cors_enabled_when_enable_cors_attribute_present(bool isCorsEnabled, string[] attributes)
        {
            _sut.Definition = string.Join(", ", attributes);

            _sut.CorsEnabled.ShouldEqual(isCorsEnabled);
        }

        [Fact]
        public void csrf_none_by_default()
        {
            _sut.CsrfProtection.ShouldEqual(Csrf.None);
        }
    }
}