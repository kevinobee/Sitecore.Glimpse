namespace Sitecore.Glimpse
{
    public class CultureSection : BaseSection
    {
        public CultureSection(RequestData requestData)
            : base(requestData, DataKey.Culture, "Culture")
        {
        }
    }

    public class LanguageSection : BaseSection
    {
        public LanguageSection(RequestData requestData)
            : base(requestData, DataKey.Language, "Language")
        {
        }
    }

    public class ItemVisualizationSection : BaseSection
    {
        public ItemVisualizationSection(RequestData requestData)
            : base(requestData, DataKey.ItemVisualization, "Visualization")
        {
        }
    }

    public class DiagnosticsSection : BaseSection
    {
        public DiagnosticsSection(RequestData requestData)
            : base(requestData, DataKey.Diagnostics, "Diagnostics")
        {
        }
    }

    public class DeviceSection : BaseSection
    {
        public DeviceSection(RequestData requestData)
            : base(requestData, DataKey.Device, "Device")
        {
        }
    }

    public class ItemTemplateSection : BaseSection
    {
        public ItemTemplateSection(RequestData requestData)
            : base(requestData, DataKey.ItemTemplate, "Template")
        {
        }
    }

    public class DomainSection : BaseSection
    {
        public DomainSection(RequestData requestData)
            : base(requestData, DataKey.Domain, "Domain")
        {
        }
    }

    public class DatabaseSection : BaseSection
    {
        public DatabaseSection(RequestData requestData)
            : base(requestData, DataKey.Database, "Database")
        {
        }
    }

    public class UserSection : BaseSection
    {
        public UserSection(RequestData requestData)
            : base(requestData, DataKey.User, "User")
        {
        }
    }

    public class SiteSection : BaseSection
    {
        public SiteSection(RequestData requestData)
            : base(requestData, DataKey.Site, "Site")
        {
        }
    }

    public class ItemSection : BaseSection
    {
        public ItemSection(RequestData requestData)
            : base(requestData, DataKey.Item, "Item")
        {
        }
    }
}
