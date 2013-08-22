using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetItemPropertiesLite(Sitecore.Data.Items.Item i)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Item Property", "Value" },
                    new object[] { "Name", i.Name},
                    new object[] { "Display Name" , i.DisplayName },
                    new object[] { "Language Name" , i.Language.Name },
                    new object[] { "Template Name" , i.Template.Name },
                    new object[] { "Parent Name" , (i.Parent != null) ? i.Parent.Name : string.Empty },
                    new object[] { "Full Path" , i.Paths.FullPath },
                    new object[] { "Version Number" , i.Version.Number },
                    new object[] { "ID", i.ID.Guid},
                    new object[] { "Parent ID", i.ParentID.Guid},
                    new object[] { "Template ID", i.TemplateID.Guid},
                };
            return results;
        }

        public static List<object[]> GetItemPropertiesFull(Sitecore.Data.Items.Item i)
        {
            var results = GetItemPropertiesLite(i);

            results.Add(new object[] { "Fields", GetItemFieldPropertiesFull(i) });
            results.Add(new object[] { "Extended Properties", GetItemExtendedProperties(i) });

            return results;
        }

        public static object[] GetItemExtendedProperties(Sitecore.Data.Items.Item i)
        {
            return new object[] 
                {
                    new object[] { "Item Property" , "Value" },
                    new object[] { "Key" , i.Key },
                    new object[] { "Has Children" , i.HasChildren },
                    new object[] { "Children" , (i.Children != null) ? i.Children.Count : 0 },
                    new object[] { "Branch Name" , (i.Branch != null) ? i.Branch.Name : string.Empty },
                    new object[] { "Short Description" , i.Appearance.ShortDescription },
                    new object[] { "Long Description" , i.Appearance.LongDescription },
                    new object[] { "Branch Id", i.BranchId.Guid},
                    new object[] { "Originator Id", i.OriginatorId.Guid},
                    new object[] { "Uri" , i.Uri },
                    new object[] { "Full Path" , i.Paths.FullPath },
                    new object[] { "Long ID" , i.Paths.LongID },
                    new object[] { "Hidden" , i.Appearance.Hidden },
                    new object[] { "Read Only" , i.Appearance.ReadOnly },
                    new object[] { "Sort order" , i.Appearance.Sortorder },
                    new object[] { "Style" , i.Appearance.Style },
                    new object[] { "Is Clone" , i.IsClone },
                    new object[] { "Is Item Clone" , i.IsItemClone },
                    new object[] { "Source Uri" , i.SourceUri },
                    new object[] { "Created" , i.Statistics.Created },
                    new object[] { "Created By" , i.Statistics.CreatedBy },
                    new object[] { "Updated" , i.Statistics.Updated },
                    new object[] { "Updated By" , i.Statistics.UpdatedBy },
                    new object[] { "Revision" , i.Statistics.Revision }
                };
        }

        public static List<object[]> GetItemFieldPropertiesFull(Sitecore.Data.Items.Item i)
        {
            var result = new List<object[]>()
                {
                    new object[] { "Section" , "Fields" }
                };
            
            i.Fields.ReadAll();

            var groupedFields = i.Fields
                .GroupBy(f => new { f.SectionSortorder, f.SectionDisplayName })
                .OrderBy(g => g.Key.SectionSortorder)
                .ThenBy(g => g.Key.SectionDisplayName);

            foreach (var group in groupedFields)
            {
                var results = new List<object[]>()
                {
                    new [] { "Field Title", "Field Type", "Value", "Contains Standard Value", "Inherits Value", "Unversioned", "Shared" }
                };

                results.AddRange(
                    group.OrderBy(f => f.Sortorder)
                        .Select(f =>
                            new object[] { !string.IsNullOrEmpty(f.Title) ? f.Title : f.DisplayName, f.Type, f.Value, f.ContainsStandardValue, f.InheritsValueFromOtherItem, f.Unversioned, f.Shared }));

                result.Add(new object[] {
                    string.Format("{0}", group.Key.SectionDisplayName),
                    results
                });

            }

            return result;
        }

    }
}
