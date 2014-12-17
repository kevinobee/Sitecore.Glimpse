using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class ServerSection : BaseSection
    {
        public ServerSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var section = new TabSection("", GetSectionHeader());

            var licenseSection = new LicenseSection(RequestData).Create();
            if (licenseSection != null)
            {
                section.AddRow().Column("License").Column(licenseSection);
            }

            var userListSection = new UserListSection(RequestData).Create();
            if (userListSection != null)
            {
                section.AddRow().Column("Current Users").Column(userListSection);
            }
            
            var servicesSection = new ServicesSection(RequestData).Create();
            if (servicesSection != null)
            {
                section.AddRow().Column("Sitecore Services").Column(servicesSection);
            }

            return section;
        }

        private string GetSectionHeader()
        {
            var versionInfo = (string)RequestData[DataKey.VersionInfo];

            return (! string.IsNullOrEmpty(versionInfo)) ? versionInfo : "Value";
        }
    }
}