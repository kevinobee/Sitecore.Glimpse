using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class CultureSection : BaseSection
    {
        public CultureSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Culture);
        }
    }

    public class LanguageSection : BaseSection
    {
        public LanguageSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Language);
        }
    }

    public class ItemVisualizationSection : BaseSection
    {
        public ItemVisualizationSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.ItemVisualization);
        }
    }

    public class DiagnosticsSection : BaseSection
    {
        public DiagnosticsSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Diagnostics);
        }
    }

    public class PageModeSection : BaseSection
    {
        public PageModeSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.PageMode);
        }
    }

    public class DeviceSection : BaseSection
    {
        public DeviceSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Device);
        }
    }

    public class ItemTemplateSection : BaseSection
    {
        public ItemTemplateSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.ItemTemplate);
        }
    }

    public class DomainSection : BaseSection
    {
        public DomainSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Domain);
        }
    }

    public class DatabaseSection : BaseSection
    {
        public DatabaseSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Database);
        }
    }

    public class UserSection : BaseSection
    {
        public UserSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.User);
        }
    }

    public class SiteSection : BaseSection
    {
        public SiteSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            return CreateSection(DataKey.Site);
        }
    }
}
