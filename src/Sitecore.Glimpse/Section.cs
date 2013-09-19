namespace Sitecore.Glimpse
{
    public class CultureSection : BaseSection
    {
        public CultureSection(RequestData requestData)
            : base(requestData, DataKey.Culture)
        {
        }
    }

    public class LanguageSection : BaseSection
    {
        public LanguageSection(RequestData requestData)
            : base(requestData, DataKey.Language)
        {
        }
    }

    public class ItemVisualizationSection : BaseSection
    {
        public ItemVisualizationSection(RequestData requestData)
            : base(requestData, DataKey.ItemVisualization)
        {
        }
    }

    public class DiagnosticsSection : BaseSection
    {
        public DiagnosticsSection(RequestData requestData)
            : base(requestData, DataKey.Diagnostics)
        {
        }
    }

    public class DeviceSection : BaseSection
    {
        public DeviceSection(RequestData requestData)
            : base(requestData, DataKey.Device)
        {
        }
    }

    public class ItemTemplateSection : BaseSection
    {
        public ItemTemplateSection(RequestData requestData)
            : base(requestData, DataKey.ItemTemplate)
        {
        }
    }

    public class DomainSection : BaseSection
    {
        public DomainSection(RequestData requestData)
            : base(requestData, DataKey.Domain)
        {
        }
    }

    public class DatabaseSection : BaseSection
    {
        public DatabaseSection(RequestData requestData)
            : base(requestData, DataKey.Database)
        {
        }
    }

    public class UserSection : BaseSection
    {
        public UserSection(RequestData requestData)
            : base(requestData, DataKey.User)
        {
        }
    }

    public class SiteSection : BaseSection
    {
        public SiteSection(RequestData requestData)
            : base(requestData, DataKey.Site)
        {
        }
    }
}
