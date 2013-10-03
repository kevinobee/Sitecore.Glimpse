using Glimpse.Core.Extensibility;
using Moq;
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
        public void Provide_a_sitecore_tab()
        {
            Assert.IsAssignableFrom<ITab>(_sut);
        }
    
        [Fact]
        public void Name_tab_Sitecore()
        {
            Assert.Equal("Sitecore", _sut.Name);
        }

        [Fact]
        public void Implements_Glimpse_IDocumentation()
        {
            Assert.IsAssignableFrom<IDocumentation>(_sut);
        }

        [Fact]
        public void Return_Wiki_Url_For_IDocumentation()
        {
            Assert.Equal("http://kevinobee.github.io/Sitecore.Glimpse/", _sut.DocumentationUri);
        }

        [Fact]
        public void Returns_null_if_no_data_available()
        {
            _requestDataProvider.Setup(x => x.GetData()).Returns(new RequestData());

            var data = _sut.GetData(null);

            Assert.Null(data);
        }

        [Fact]
        public void Returns_null_if_Sitecore_Item_not_in_request_data()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.Campaign, "Foo");

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = _sut.GetData(null);

            Assert.Null(data);
        }

        [Fact]
        public void Show_Item_Path_and_Template_as_first_row_of_tab_data()
        {
            var requestData = new RequestData();
            var fieldList = new FieldList();
            fieldList.AddField("Full Path", "/sitecore/content/foo");
            fieldList.AddField("Template Name", "Bar");
            requestData.Add(DataKey.Item, fieldList);

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            dynamic data = _sut.GetData(null);

            string summaryRow = data.Rows[0].Columns[1].Data;
            Assert.Contains("/sitecore/content/foo", summaryRow);
            Assert.Contains("Bar", summaryRow);
        }
    }
}
