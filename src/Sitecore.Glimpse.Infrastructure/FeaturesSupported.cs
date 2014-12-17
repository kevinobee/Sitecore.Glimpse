using System.Diagnostics;
using Sitecore.Data.Items;

namespace Sitecore.Glimpse.Infrastructure
{
    internal static class FeaturesSupported
    {
        private static ProductVersion _version;

        public static ProductVersion Version
        {
            get
            {
                return _version ?? (_version = GetVersionInfo());
            }

            set { _version = value; }
        }

        private static ProductVersion GetVersionInfo()
        {
            var fileVersion = FileVersionInfo.GetVersionInfo(typeof (Item).Assembly.Location);
            return new ProductVersion
                {
                    MajorPart = fileVersion.ProductMajorPart,
                    MinorPart = fileVersion.ProductMinorPart
                };
        }

        /// <summary>
        /// Sitecore support for Clones introduced in 6.3
        /// </summary>
        public static bool Clones
        {
            get
            {
                if (Version.MajorPart >= 7) return true;

                return ((Version.MajorPart >= 6) && (Version.MinorPart >= 3));
            }
        }

        /// <summary>
        /// Sitecore support for Clones introduced in 7.5
        /// </summary>
        public static bool ServicesClient
        {
            get
            {
                if (Version.MajorPart >= 8) return true;

                return ((Version.MajorPart >= 7) && (Version.MinorPart >= 5));
            }
        }
    }

    public class ProductVersion
    {
        public int MajorPart { get; set; }
        public int MinorPart { get; set; }
    }
}