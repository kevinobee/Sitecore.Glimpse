using Glimpse.Core.Tab.Assist;

namespace Sitecore.Glimpse
{
    public class ItemSection : BaseSection
    {
        public ItemSection(RequestData requestData)
            : base(requestData)
        {
        }

        public override TabSection Create()
        {
            var section = new TabSection("Item", "Value");

            var itemSection = CreateSection(DataKey.Item);
            if (itemSection != null)
            {
                section.AddRow().Column("Properties").Column(itemSection);
            }

            var itemTemplate = new ItemTemplateSection(RequestData).Create();
            if (itemTemplate != null) section.AddRow().Column("Template").Column(itemTemplate);

            var itemVisualization = new ItemVisualizationSection(RequestData).Create();
            if (itemVisualization != null) section.AddRow().Column("Visualization").Column(itemVisualization);

            return section;
        }
    }
}