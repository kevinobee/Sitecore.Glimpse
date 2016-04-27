using Glimpse.Core.Extensibility;
using Moq;

using Should;

using Xunit;

namespace Sitecore.Glimpse.Test
{
    public class SitecoreTabShould
    {
        private readonly SitecoreTab _sut;
        private readonly Mock<ISitecoreRequest> _requestDataProvider;

        public SitecoreTabShould()
        {
            _requestDataProvider = new Mock<ISitecoreRequest>();

            _sut = new SitecoreTab(_requestDataProvider.Object);
        }

        [Fact]
        public void ProvideSitecoreTab()
        {
            _sut.ShouldImplement<ITab>();
        }
    
        [Fact]
        public void NameTabSitecore()
        {
            _sut.Name
                .ShouldEqual("Sitecore");
        }

        [Fact]
        public void ImplementsGlimpseIDocumentation()
        {
            _sut.ShouldImplement<IDocumentation>();
        }

        [Fact]
        public void ReturnWikiUrlForIDocumentation()
        {
            _sut.DocumentationUri
                .ShouldEqual("http://kevinobee.github.io/Sitecore.Glimpse/");
        }

        [Fact]
        public void ReturnsNullIfNoDataAvailable()
        {
            _requestDataProvider
                .Setup(x => x.GetData())
                .Returns(new RequestData());

            _sut.GetData(null)
                .ShouldBeNull();
        }

        [Fact]
        public void ReturnsNullIfSitecoreItemNotInRequestData()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.Campaign, "Foo");

            _requestDataProvider
                .Setup(x => x.GetData())
                .Returns(requestData);

            _sut.GetData(null)
                .ShouldBeNull();
        }

        [Fact]
        public void ShowItemPathAndTemplateAsFirstRowOfTabData()
        {
            var requestData = new RequestData();
            var fieldList = new FieldList();
            fieldList.AddField("Full Path", "/sitecore/content/foo");
            fieldList.AddField("Template Name", "Bar");
            requestData.Add(DataKey.Item, fieldList);

            _requestDataProvider
                .Setup(x => x.GetData())
                .Returns(requestData);

            dynamic data = _sut.GetData(null);

            string summaryRow = data.Rows[0].Columns[1].Data;

            summaryRow.ShouldContain("/sitecore/content/foo");
            summaryRow.ShouldContain("Bar");
        }
    }
}