using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetTemplatePropertiesFull(Sitecore.Data.Items.TemplateItem ti)
        {
            var ownfields = GetTemplateFieldProperties(ti.OwnFields);                  
            var fields = GetTemplateFieldProperties(ti.Fields);
            var results = new List<object[]>()
                {
                    new object[] { "Template Property", "Value" },
                    new object[] { "Name", ti.Name},
                    new object[] { "Display Name", ti.DisplayName},
                    new object[] { "Full Name", ti.FullName},
                    new object[] { "ID", ti.ID.Guid},
                    new object[] { "Base Templates", ti.BaseTemplates.Select(t => t.Name) },
                    new object[] { "Standard Values", ti.StandardValues != null ? ti.StandardValues.Paths.FullPath : string.Empty },
                    new object[] { "Own Fields", ownfields},
                    new object[] { "Fields", fields },
                    new object[] { "Template InnerItem", GetItemPropertiesLite(ti.InnerItem)},

                };
            return results;
        }

        private static List<object[]> GetTemplateFieldProperties(Sitecore.Data.Items.TemplateFieldItem[] fields)
        {
            var groupedResults = new List<object[]>()
            {
                new [] { "Section", "Fields" }
            };

            var groupedFields = fields
                .GroupBy(f => new { f.Section.Sortorder, f.Section.DisplayName })
                .OrderBy(g => g.Key.Sortorder)
                .ThenBy(g => g.Key.DisplayName);
            
            foreach (var group in groupedFields )
            {
                var results = new List<object[]>()
                {
                    new [] { "Field Name", "Title", "Field Type", "Unversioned", "Shared", "Source" }
                };

                results.AddRange(
                    group.OrderBy(f => f.Sortorder)
                        .Select(f =>
                            new object[] { f.Name, f.Title, f.Type, f.Unversioned, f.Shared, f.Source }));

                groupedResults.Add(new object[] {
                    string.Format("{0}", group.Key.DisplayName),
                    results
                });
                
            }
            
            return groupedResults;
        }

    }
}
