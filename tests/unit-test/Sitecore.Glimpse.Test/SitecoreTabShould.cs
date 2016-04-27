using Glimpse.Core.Extensibility;
using Moq;

using Should;

using Xunit;

namespace Sitecore.Glimpse.Test
{
    public class SitecoreTabShould
    {
        private readonly SitecoreTab _sut;

        private readonly RequestData _requestData;

        public SitecoreTabShould()
        {
            var requestDataProvider = new Mock<ISitecoreRequest>();

            _requestData = new RequestData();

            requestDataProvider
                .Setup(x => x.GetData())
                .Returns(_requestData);

            _sut = new SitecoreTab(requestDataProvider.Object);
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
            _sut.GetData(null)
                .ShouldBeNull();
        }

        [Fact]
        public void ReturnsNullIfSitecoreItemNotInRequestData()
        {
            _requestData.Add(DataKey.Campaign, "Foo");

            _sut.GetData(null)
                .ShouldBeNull();
        }

        [Fact]
        public void ShowItemPathAndTemplateAsFirstRowOfTabData()
        {
            var fieldList = new FieldList();
            fieldList.AddField("Full Path", "/sitecore/content/foo");
            fieldList.AddField("Template Name", "Bar");
            
            _requestData.Add(DataKey.Item, fieldList);

            dynamic data = _sut.GetData(null);

            string summaryRow = data.Rows[0].Columns[1].Data;

            summaryRow.ShouldContain("/sitecore/content/foo");
            summaryRow.ShouldContain("Bar");
        }
    }
}