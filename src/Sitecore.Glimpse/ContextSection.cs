using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class ContextSection : BaseSection
    {
        private readonly RequestData _requestData;

        public ContextSection(RequestData requestData)
        {
            _requestData = requestData;
        }

        public override TabSection Create()
        {
            var section = new TabSection("Context", "Value");

            var siteSection = new SiteSection(_requestData).Create();
            if (siteSection != null) section.AddRow().Column("Site").Column(siteSection);

            var databaseSection = new DatabaseSection(_requestData).Create();
            if (databaseSection != null) section.AddRow().Column("Database").Column(databaseSection);

            var deviceSection = new DeviceSection(_requestData).Create();
            if (deviceSection != null) section.AddRow().Column("Device").Column(deviceSection);

            var domainSection = new DomainSection(_requestData).Create();
            if (domainSection != null) section.AddRow().Column("Domain").Column(domainSection);

            var languageSection = new LanguageSection(_requestData).Create();
            if (languageSection != null) section.AddRow().Column("Language").Column(languageSection);

            var cultureSection = new CultureSection(_requestData).Create();
            if (cultureSection != null) section.AddRow().Column("Culture").Column(cultureSection);

            var userSection = new UserSection(_requestData).Create();
            if (userSection != null) section.AddRow().Column("User").Column(userSection);

            var requestSection = new RequestSection(_requestData).Create();
            if (requestSection != null) section.AddRow().Column("Request").Column(requestSection);
            
            var diagnosticsSection = new DiagnosticsSection(_requestData).Create();
            if (diagnosticsSection != null) section.AddRow().Column("Diagnostics").Column(diagnosticsSection);

            return section;
        }
    }
}