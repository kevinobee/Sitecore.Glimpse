using System;
using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;
using Moq;
using Sitecore.Glimpse.Analytics;
using Sitecore.Glimpse.Model.Analytics;
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
        public void Return_null_if_analysis_overview_information_is_missing()
        {
            var requestData = new RequestData();
            var fieldList = new FieldList();
            fieldList.AddField("Foo", "Bar");
            requestData.Add(DataKey.Item, fieldList);

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = _sut.GetData(null);

            Assert.Null(data);
        }

        [Fact]
        public void Show_Visitor_Insight_as_summary_of_tab_data_by_default()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.IsNewVisitor, true);
            requestData.Add(DataKey.EngagementValue, 0);
            requestData.Add(DataKey.TrafficType, "Returning");

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            dynamic data = _sut.GetData(null);

            string column1 = data.Rows[0].Columns[0].Data; 
            string column2 = data.Rows[0].Columns[1].Data;

            Assert.Contains("Visitor", column1);
            Assert.Contains("Insight", column2);
        }

        [Fact]
        public void Not_include_page_views_if_no_page_visits_recorded()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.IsNewVisitor, true);
            requestData.Add(DataKey.EngagementValue, 0);
            requestData.Add(DataKey.TrafficType, "Returning");

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = (TabSection) _sut.GetData(null);

            var sectionFound = data.Rows.Any(x => (string)x.Columns.First().Data == "Page Views");
            Assert.False(sectionFound);
        }

        [Fact]
        public void Include_page_views_if_page_visits_recorded()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.IsNewVisitor, true);
            requestData.Add(DataKey.EngagementValue, 0);
            requestData.Add(DataKey.TrafficType, "Returning");

            requestData.Add(DataKey.LastPages, new[]
                {
                    new PageHolder(1,Guid.NewGuid(), DateTime.Now, "http://microsoft.com/")
                } );

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = (TabSection)_sut.GetData(null);

            var sectionFound = data.Rows.Any(x => (string)x.Columns.First().Data == "Page Views");
            Assert.True(sectionFound);
        }

        [Fact]
        public void Not_include_goal_section_if_no_goals_recorded()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.IsNewVisitor, true);
            requestData.Add(DataKey.EngagementValue, 0);
            requestData.Add(DataKey.TrafficType, "Returning");

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = (TabSection)_sut.GetData(null);

            var sectionFound = data.Rows.Any(x => (string)x.Columns.First().Data == "Goals");
            Assert.False(sectionFound);
        }

        [Fact]
        public void Include_goals_section_if_goals_recorded()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.IsNewVisitor, true);
            requestData.Add(DataKey.EngagementValue, 0);
            requestData.Add(DataKey.TrafficType, "Returning");

            requestData.Add(DataKey.Goals, new[]
                {
                    new Goal { Name = "Foo", Timestamp = DateTime.Now.AddMinutes(-10)}
                });

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = (TabSection)_sut.GetData(null);

            var sectionFound = data.Rows.Any(x => (string)x.Columns.First().Data == "Goals");
            Assert.True(sectionFound);
        }

        [Fact]
        public void Not_include_profiles_section_if_no_profile_recorded()
        {
            var requestData = new RequestData();
            requestData.Add(DataKey.IsNewVisitor, true);
            requestData.Add(DataKey.EngagementValue, 0);
            requestData.Add(DataKey.TrafficType, "Returning");

            var noProfileData = new Profile[] {};
            requestData.Add(DataKey.Profiles, noProfileData);

            _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

            var data = (TabSection)_sut.GetData(null);

            var sectionFound = data.Rows.Any(x => (string)x.Columns.First().Data == "Profiles");
            Assert.False(sectionFound);
        }

        //[Fact]
        //public void Include_profiles_section_if_no_matching_profiles_recorded()
        //{
        //    var requestData = new RequestData();
        //    requestData.Add(DataKey.IsNewVisitor, true);
        //    requestData.Add(DataKey.EngagementValue, 0);
        //    requestData.Add(DataKey.TrafficType, "Returning");

        //    requestData.Add(DataKey.Profiles, new[]
        //        {
        //            new Profile { Name = "Foo", Dimension = "Product Interest", IsMatch = false },
        //            new Profile { Name = "Bar", Dimension = "Product Interest", IsMatch = false }
        //        });

        //    _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

        //    var data = (TabSection)_sut.GetData(null);

        //    var sectionFound = data.Rows.Any(x => x.Columns.First().Data.ToString().Contains("Profiles"));
        //    Assert.True(sectionFound);
        //}

        //[Fact]
        //public void Include_profiles_section_if_profiles_recorded()
        //{
        //    var requestData = new RequestData();
        //    requestData.Add(DataKey.IsNewVisitor, true);
        //    requestData.Add(DataKey.EngagementValue, 0);
        //    requestData.Add(DataKey.TrafficType, "Returning");

        //    requestData.Add(DataKey.Profiles, new[]
        //        {
        //            new Profile { Name = "Foo", Dimension = "Product Interest", IsMatch = false },
        //            new Profile { Name = "Bar", Dimension = "Product Interest", IsMatch = true }
        //        });

        //    _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

        //    var data = (TabSection)_sut.GetData(null);

        //    var sectionFound = data.Rows.Any(x => x.Columns.First().Data.ToString().Contains("Profiles"));
        //    Assert.True(sectionFound);
        //}

        //[Fact]
        //public void Display_multiple_dimensions_for_profiles_section()
        //{
        //    var requestData = new RequestData();
        //    requestData.Add(DataKey.IsNewVisitor, true);
        //    requestData.Add(DataKey.EngagementValue, 0);
        //    requestData.Add(DataKey.TrafficType, "Returning");

        //    requestData.Add(DataKey.Profiles, new[]
        //        {
        //            new Profile { Name = "Foo", Dimension = "Product Interest", IsMatch = false },
        //            new Profile { Name = "Bar", Dimension = "Product Interest", IsMatch = true },
        //            new Profile { Name = "Low", Dimension = "Income", IsMatch = false },
        //            new Profile { Name = "Medium", Dimension = "Income", IsMatch = false },
        //            new Profile { Name = "High", Dimension = "Income", IsMatch = true }
        //        });

        //    _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

        //    var data = (TabSection) _sut.GetData(null);

        //    var profileData =  (TabSection) data.Rows.ElementAt(2).Columns.ElementAt(1).Data;
        //    var incomeTitleCell = profileData.Rows.ElementAt(2).Columns.ElementAt(0).Data;

        //    Assert.Equal("Income", incomeTitleCell);
        //}

        //[Fact]
        //public void Show_matched_patters_as_first_row_of_tab_data()
        //{
        //    var requestData = new RequestData();
        //    requestData.Add(DataKey.IsNewVisitor, true);
        //    requestData.Add(DataKey.EngagementValue, 0);
        //    requestData.Add(DataKey.TrafficType, "Returning");

        //    requestData.Add(DataKey.Profiles, new[]
        //        {
        //            new Profile { Name = "Foo", Dimension = "Product Interest", IsMatch = false },
        //            new Profile { Name = "Bar", Dimension = "Product Interest", IsMatch = true },
        //            new Profile { Name = "Low", Dimension = "Income", IsMatch = false },
        //            new Profile { Name = "Medium", Dimension = "Income", IsMatch = false },
        //            new Profile { Name = "High", Dimension = "Income", IsMatch = true }
        //        });

        //    _requestDataProvider.Setup(x => x.GetData()).Returns(requestData);

        //    dynamic data = _sut.GetData(null);

        //    string summaryRow = data.Rows[0].Columns[1].Data;
        //    Assert.Equal("Product Interest: Bar, Income: High", summaryRow);
        //}
    }
}