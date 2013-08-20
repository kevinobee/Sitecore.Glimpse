using Glimpse.Core.Extensibility;

using Xunit;

namespace Sitecore.Glimpse.Test
{
    public class SitecoreGlimpseShould
    {
        [Fact]
        public void Provide_a_sitecore_tab()
        {
            var sut = new SitecoreTab();

            Assert.IsAssignableFrom<ITab>(sut);
        }
    }
}
