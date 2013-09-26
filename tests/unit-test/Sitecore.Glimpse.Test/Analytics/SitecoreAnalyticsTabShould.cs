using Glimpse.Core.Extensibility;
using Moq;
using Sitecore.Glimpse.Analytics;
using Xunit;

namespace Sitecore.Glimpse.Test.Analytics
{
    public class SitecoreAnalyticsTabShould
    {
        private readonly SitecoreAnalyticsTab _sut;
        private readonly Mock<ISitecoreRequest> _requestDataProvider;

        public SitecoreAnalyticsTabShould()
        {
            _requestDataProvider = new Mock<ISitecoreRequest>();
            _sut = new SitecoreAnalyticsTab(_requestDataProvider.Object);
        }

        [Fact]
        public void Provide_a_sitecore_tab()
        {
            Assert.IsAssignableFrom<ITab>(_sut);
        }

        [Fact]
        public void Name_tab_Sitecore()
        {
            Assert.Equal("Sitecore Analytics", _sut.Name);
        }

        [Fact]
        public void Implements_Glimpse_IDocumentation()
        {
            Assert.IsAssignableFrom<IDocumentation>(_sut);
        }

        [Fact]
        public void Return_Wiki_Url_For_IDocumentation()
        {
            Assert.Equal("https://github.com/kevinobee/Sitecore.Glimpse/wiki", _sut.DocumentationUri);
        }

        [Fact]
        public void Returns_null_if_no_data_available()
        {
            _requestDataProvider.Setup(x => x.GetData()).Returns(new RequestData());

            var data = _sut.GetData(null);

            Assert.Null(data);
        }
    }
}