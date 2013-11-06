using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test
{
    public class FeaturesSupportedShould
    {
        [Fact]
        public void Show_clones_not_supported_for_version_6_2()
        {
            var productVersion = new ProductVersion { MajorPart = 6, MinorPart = 2 };
            FeaturesSupported.Version = productVersion;

            Assert.False(FeaturesSupported.Clones);
        }

        [Fact]
        public void Show_clones_supported_for_version_6_3()
        {
            var productVersion = new ProductVersion { MajorPart = 6, MinorPart = 3 };
            FeaturesSupported.Version = productVersion;

            Assert.True(FeaturesSupported.Clones);
        }

        [Fact]
        public void Show_clones_supported_for_version_7_0()
        {
            var productVersion = new ProductVersion { MajorPart = 7, MinorPart = 0 };
            FeaturesSupported.Version = productVersion;

            Assert.True(FeaturesSupported.Clones);
        }
    }
}
