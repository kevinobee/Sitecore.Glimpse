using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetVisualizationPropertiesFull(Sitecore.Data.Items.ItemVisualization iv, Sitecore.Data.Items.DeviceItem di)
        {
            var layoutItem = iv.GetLayout(di);
            var renderings = iv.GetRenderings(di, true);


            var renderingResults = new List<object[]>()
            {
                new [] { "Placeholder", "Display Name", "Unique ID", "Rendering ID", "Cacheable", "Conditions", "DataSource", "Parameters", "MultiVariateTest" }
            };

            foreach (var rendering in renderings)
            {
                renderingResults.Add(new object[] {
                    rendering.Settings.Placeholder,
                    rendering.RenderingItem != null && !string.IsNullOrEmpty(rendering.RenderingItem.DisplayName) 
                        ? rendering.RenderingItem.DisplayName : string.Empty,
                    rendering.UniqueId,                    
                    rendering.RenderingID.Guid.ToString(),
                    rendering.Settings.Caching.Cacheable,
                    rendering.Settings.Conditions,
                    rendering.Settings.DataSource,
                    rendering.Settings.Parameters,
                    rendering.Settings.MultiVariateTest
                });
            }

            var results = new List<object[]>()
                {
                    new object[] { "Visualization Property", "Value" },
                    new object[] { "Layout", 
                        new object[] {
                            new object[] { "Display Name", layoutItem.DisplayName },
                            new object[] { "File Path", layoutItem.FilePath },
                            new object[] { "ID", layoutItem.ID.Guid.ToString() },
                            // new object[] { "Control", layoutItem.Control },
                        }
                    },
                    new object[] { "Renderings", renderingResults.ToArray() }
                };
            return results;
        }

    }
}
