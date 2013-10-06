using System.Diagnostics;
using Sitecore.Data.Items;

namespace Sitecore.Glimpse.Infrastructure
{
    static class FeaturesSupported
    {
        private static FileVersionInfo _version;

        private static FileVersionInfo Version
        {
            get
            {
                return _version ?? (_version = FileVersionInfo.GetVersionInfo(typeof (Item).Assembly.Location));
            }
        }

        /// <summary>
        /// Sitecore support for Clones introduced in 6.3
        /// </summary>
        public static bool Clones
        {
            get { return ((Version.ProductMajorPart >= 6) && (Version.ProductMinorPart >= 3)); }
        }
    }
}