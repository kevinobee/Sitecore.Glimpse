using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class ContextSection : BaseSection
    {
        public ContextSection(RequestData requestData) : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var section = new TabSection("Context", "Value");

            var siteSection = new SiteSection(RequestData).Create();
            if (siteSection != null) section.AddRow().Column("Site").Column(siteSection);

            var databaseSection = new DatabaseSection(RequestData).Create();
            if (databaseSection != null) section.AddRow().Column("Database").Column(databaseSection);

            var deviceSection = new DeviceSection(RequestData).Create();
            if (deviceSection != null) section.AddRow().Column("Device").Column(deviceSection);

            var domainSection = new DomainSection(RequestData).Create();
            if (domainSection != null) section.AddRow().Column("Domain").Column(domainSection);

            var languageSection = new LanguageSection(RequestData).Create();
            if (languageSection != null) section.AddRow().Column("Language").Column(languageSection);

            var cultureSection = new CultureSection(RequestData).Create();
            if (cultureSection != null) section.AddRow().Column("Culture").Column(cultureSection);

            var userSection = new UserSection(RequestData).Create();
            if (userSection != null) section.AddRow().Column("User").Column(userSection);

            var requestSection = new RequestSection(RequestData).Create();
            if (requestSection != null) section.AddRow().Column("Request").Column(requestSection);

            var diagnosticsSection = new DiagnosticsSection(RequestData).Create();
            if (diagnosticsSection != null) section.AddRow().Column("Diagnostics").Column(diagnosticsSection);

            return section;
        }
    }
}