using System;

using Sitecore.SecurityModel.License;
using Sitecore.Xml;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class LicenseReader
    {
        public LicenseInfo GetInfo()
        {
            var license = License.VerifiedLicense();
            if (license == null)
            {
                return null;
            }

            var licenseNode = license.SelectSingleNode("/verifiedlicense/license");
            var licenseInfo = new LicenseInfo
            {
                Id = XmlUtil.GetChildValue("id", licenseNode),
                Expiration = DateUtil.IsoDateToDateTime(XmlUtil.GetChildValue("expiration", licenseNode)),
                Version = int.Parse(XmlUtil.GetChildValue("version", licenseNode)),
                Licensee = XmlUtil.GetChildValue("licensee", licenseNode),
                Country = XmlUtil.GetChildValue("country", licenseNode)
            };

            return licenseInfo;
        }
    }

    internal class LicenseInfo
    {
        public string Id { get; set; }
        public DateTime Expiration { get; set; }
        public int Version { get; set; }
        public string Licensee { get; set; }
        public string Country { get; set; }
    }
}