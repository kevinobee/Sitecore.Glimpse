using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class ItemSection : BaseSection
    {
        private readonly RequestData _requestData;

        public ItemSection(RequestData requestData)
            : base(requestData, DataKey.Item)
        {
            _requestData = requestData;
        }

        public override TabSection Create()
        {
            var section = new TabSection("Item", "Value");

            var itemSection = base.Create();
            if (itemSection != null) section.AddRow().Column("Properties").Column(itemSection);

            var itemTemplate = new ItemTemplateSection(_requestData).Create();
            if (itemTemplate != null) section.AddRow().Column("Template").Column(itemTemplate);

            var itemVisualization = new ItemVisualizationSection(_requestData).Create();
            if (itemVisualization != null) section.AddRow().Column("Visualization").Column(itemVisualization);

            return section;
        }
    }
}