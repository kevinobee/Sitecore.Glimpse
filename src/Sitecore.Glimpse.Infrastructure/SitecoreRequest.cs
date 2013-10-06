using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Sitecore.Data.Items;
using Sitecore.Layouts;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreRequest : ISitecoreRequest
    {
        private readonly ILog _logger;

        public SitecoreRequest(ILog logger)
        {
            _logger = logger;
        }

        public SitecoreRequest() : this(new TraceLogger())
        {
        }

        public RequestData GetData()
        {
            try
            {
                return GetSitecoreData();
            }
            catch (Exception exception)
            {
                _logger.Write(string.Format("Failed to load Sitecore Glimpse data - {0}", exception.Message));
            }

            return new RequestDataNotLoaded();
        }

        private static RequestData GetSitecoreData()
        {
            var data = new RequestData();

            data.Add(DataKey.Request, GetRequest());
            data.Add(DataKey.Diagnostics, GetDiagnostics());
            data.Add(DataKey.Culture, GetCulture());
            data.Add(DataKey.Language, GetLanguage());
            data.Add(DataKey.Domain, GetDomain());
            data.Add(DataKey.Device, GetDevice());
            data.Add(DataKey.User, GetUser());
            data.Add(DataKey.Database, GetDatabase());
            data.Add(DataKey.Site, GetSite());
            data.Add(DataKey.ItemVisualization, GetItemVisualization());
            data.Add(DataKey.ItemTemplate, GetItemTemplate());
            data.Add(DataKey.Item, GetItem());

            return data;
        }

        private static FieldList GetCulture()
        {
            return GetCulture(Context.Culture);
        }

        private static FieldList GetCulture(CultureInfo culture)
        {
            var data = new FieldList();

            data.AddField("Name", culture.Name);
            data.AddField("Parent", culture.Parent);
            data.AddField("Display Name", culture.DisplayName);
            data.AddField("English Name", culture.EnglishName);
            data.AddField("Native Name", culture.NativeName);
            data.AddField("Two Letter ISO Language Name", culture.TwoLetterISOLanguageName);
            data.AddField("Three Letter Windows Language Name", culture.ThreeLetterWindowsLanguageName);
            data.AddField("Three Letter ISO Language Name", culture.ThreeLetterISOLanguageName);

            return data;
        }

        private static FieldList GetLanguage()
        {
            var language = Context.Language;

            var data = new FieldList();

            data.AddField("Name", language.Name);
            data.AddField("DisplayName", language.GetDisplayName());
            data.AddField("CultureInfo", GetCulture(language.CultureInfo).Fields);

            if (language.Origin != null && language.Origin.ItemId != (Data.ID)null)
            {
                data.AddField("Origin Item Id", language.Origin.ItemId.Guid);
            }

            return data;
        }

        private static FieldList GetItemVisualization()
        {
            var itemVisualization = Context.Item.Visualization;
            var device = Context.Device;

            var layoutItem = itemVisualization.GetLayout(device);
            var renderings = itemVisualization.GetRenderings(device, true);

            var data = new FieldList();

            data.AddField("Layout", GetLayout(layoutItem).Fields);
            data.AddField("Renderings", GetRenderings(renderings));

            return data;
        }

        private static IList<object[]> GetRenderings(IEnumerable<RenderingReference> renderings)
        {
            var renderingResults = new List<object[]>
            {
                new object[] { "Placeholder", "Display Name", "Unique ID", "Rendering ID", "Cacheable", "Conditions", "DataSource", "Parameters", "MultiVariateTest" }
            };

            renderingResults.AddRange(renderings.Select(rendering => new object[]
                {
                    rendering.Settings.Placeholder, 
                    GetDisplayName(rendering),
                    rendering.UniqueId, 
                    rendering.RenderingID.Guid, 
                    rendering.Settings.Caching.Cacheable, 
                    rendering.Settings.Conditions, 
                    rendering.Settings.DataSource, 
                    rendering.Settings.Parameters, 
                    rendering.Settings.MultiVariateTest
                }));

            return renderingResults;
        }

        private static string GetDisplayName(RenderingReference rendering)
        {
            return (rendering.RenderingItem != null &&
                    !string.IsNullOrEmpty(rendering.RenderingItem.DisplayName))
                       ? rendering.RenderingItem.DisplayName
                       : string.Empty;
        }

        private static FieldList GetLayout(LayoutItem layoutItem)
        {
            var data = new FieldList();

            data.AddField("Display Name", layoutItem.DisplayName);
            data.AddField("File Path", layoutItem.FilePath);
            data.AddField("ID", layoutItem.ID.Guid.ToString());

            return data;
        }

        private static FieldList GetDiagnostics()
        {
            var diag = Context.Diagnostics;

            var data = new FieldList();

            data.AddField("Debugging", diag.Debugging);
            data.AddField("Profiling", diag.Profiling);
            data.AddField("Tracing", diag.Tracing);
            data.AddField("Show Rendering Info", diag.ShowRenderingInfo);
            data.AddField("Draw Rendering Borders", diag.DrawRenderingBorders);

            return data;
        }

        private static FieldList GetDevice()
        {
            var device = Context.Device;

            var data = new FieldList();

            data.AddField("Name", device.Name);
            data.AddField("Display Name", device.DisplayName);
            data.AddField("Id", device.ID.Guid);
            data.AddField("Query String", device.QueryString);
            data.AddField("Agent", device.Agent);

            if (device.FallbackDevice != null)
            {
                data.AddField("Fallback Device Name", device.FallbackDevice.Name);
            }

            data.AddField("Icon", device.Icon);
            data.AddField("Is Default", device.IsDefault);
            data.AddField("Is Valid", device.IsValid);

            return data;
        }

        private static FieldList GetItemTemplate()
        {
            var template = Context.Item.Template;

            var data = new FieldList();

            data.AddField("Name", template.Name);
            data.AddField("Display Name", template.DisplayName);
            data.AddField("Full Name", template.FullName);
            data.AddField("ID", template.ID.Guid);
            data.AddField("Base Templates", template.BaseTemplates.Select(t => t.Name));

            if (template.StandardValues != null)
            {
                data.AddField("Standard Values", template.StandardValues.Paths.FullPath);
            }

            data.AddField("Own Fields", GetTemplateFieldProperties(template.OwnFields));
            data.AddField("Fields", GetTemplateFieldProperties(template.Fields));
            data.AddField("Template InnerItem", GetItem(template.InnerItem).Fields);

            return data;
        }

        private static List<object[]> GetTemplateFieldProperties(IEnumerable<TemplateFieldItem> fields)
        {
            var groupedResults = new List<object[]>
            {
                new object[] { "Section", "Fields" }
            };

            var groupedFields = fields
                .GroupBy(f => new { f.Section.Sortorder, f.Section.DisplayName })
                .OrderBy(g => g.Key.Sortorder)
                .ThenBy(g => g.Key.DisplayName);

            foreach (var group in groupedFields)
            {
                var results = new List<object[]>
                {
                    new object[] { "Field Name", "Title", "Field Type", "Unversioned", "Shared", "Source" }
                };

                results.AddRange(
                    group.OrderBy(f => f.Sortorder)
                        .Select(f =>
                            new object[] { f.Name, f.Title, f.Type, f.Unversioned, f.Shared, f.Source }));

                groupedResults.Add(new object[] { string.Format("{0}", group.Key.DisplayName), results });
            }

            return groupedResults;
        }


        private static FieldList GetRequest()
        {
            var request = Context.Request;

            var data = new FieldList();

            data.AddField("FilePath", request.FilePath);
            data.AddField("ItemPath", request.ItemPath);

            if (request.QueryString.AllKeys.Any())
            {
                var queryString = new FieldList();

                foreach (var key in request.QueryString.AllKeys)
                {
                    queryString.AddField(key, request.QueryString.GetValues(key));
                }

                data.AddField("QueryString", queryString);
            }

            return data;
        }

        private static FieldList GetDomain()
        {
            var domain = Context.Domain;

            var data = new FieldList();

            data.AddField("Name", domain.Name);
            data.AddField("Is Default", domain.IsDefault);
            data.AddField("Account Prefix", domain.AccountPrefix);
            data.AddField("Anonymous User Name", domain.AnonymousUserName);
            data.AddField("Default Profile Item ID", domain.DefaultProfileItemID);
            data.AddField("Ensure Anonymous User", domain.EnsureAnonymousUser);
            data.AddField("Everyone Role Name", domain.EveryoneRoleName);
            data.AddField("Locally Managed", domain.LocallyManaged);
            data.AddField("Anonymous User Email Pattern", domain.AnonymousUserEmailPattern);
            data.AddField("Account Name Validation", domain.AccountNameValidation);
            data.AddField("Member Pattern", domain.MemberPattern);

            return data;
        }

        private static FieldList GetUser()
        {
            var user = Context.User;

            var data = new FieldList();

            data.AddField("Name", user.Name);
            data.AddField("DisplayName", user.DisplayName);
            data.AddField("Roles", user.Roles.Select(r => r.Name));
            data.AddField("Description", user.Description);
            data.AddField("Domain Name", user.GetDomainName());
            data.AddField("IsAdministrator", user.IsAdministrator);
            data.AddField("IsAuthenticated", user.IsAuthenticated);
            data.AddField("LocalName", user.LocalName);

            return data;
        }

        private static FieldList GetDatabase()
        {
            var database = Context.Database;

            var data = new FieldList();

            data.AddField("Name", database.Name);
            data.AddField("Connection String Name", database.ConnectionStringName);
            data.AddField("Read Only", database.ReadOnly);
            data.AddField("Protected", database.Protected);
            data.AddField("Security Enabled", database.SecurityEnabled);
            data.AddField("Proxies Enabled", database.ProxiesEnabled);
            data.AddField("Publish Virtual Items", database.PublishVirtualItems);
            data.AddField("HasContentItem", database.HasContentItem);

            return data;
        }

        private static FieldList GetSite()
        {
            var site = Context.Site;

            var data = new FieldList();

            data.AddField("Name", site.Name);
            data.AddField("HostName", site.HostName);
            data.AddField("TargetHostName", site.TargetHostName);
            data.AddField("Language", site.Language);
            data.AddField("Database", site.Properties["database"]);
            data.AddField("Device", site.Device);
            data.AddField("RootPath", site.RootPath);
            data.AddField("StartItem", site.StartItem);
            data.AddField("StartPath", site.StartPath);
            data.AddField("PhysicalFolder", site.PhysicalFolder);
            data.AddField("VirtualFolder", site.VirtualFolder);
            data.AddField("LoginPage", site.LoginPage);
            data.AddField("RequireLogin", site.RequireLogin);
            data.AddField("AllowDebug", site.AllowDebug);
            data.AddField("EnableAnalytics", site.EnableAnalytics);
            data.AddField("EnableDebugger", site.EnableDebugger);
            data.AddField("EnablePreview", site.EnablePreview);
            data.AddField("EnableWorkflow", site.EnableWorkflow);
            data.AddField("EnableWebEdit", site.EnableWebEdit);
            data.AddField("FilterItems", site.FilterItems);
            data.AddField("CacheHtml", site.CacheHtml);
            data.AddField("CacheMedia", site.CacheMedia);
            data.AddField("MediaCachePath", site.MediaCachePath);
            data.AddField("XmlControlPage", site.XmlControlPage);

            return data;
        }

        private static FieldList GetItem()
        {
            var item = Context.Item;

            var data = new FieldList();

            var itemValues = GetItem(item);

            foreach (var field in itemValues.Fields)
            {
                data.AddField(field.Key, field.Value);
            }

            data.AddField("Fields", GeFieldsProperties(item));
            data.AddField("Extended Properties", GetExtendedProperties(item).Fields);

            return data;
        }

        private static FieldList GetItem(Item item)
        {
            var data = new FieldList();

            data.AddField("Name", item.Name);
            data.AddField("Display Name", item.DisplayName);
            data.AddField("Language Name", item.Language.Name);
            data.AddField("Template Name", item.Template.Name);

            if (item.Parent != null)
            {
                data.AddField("Parent Name", item.Parent.Name);
            }

            data.AddField("Full Path", item.Paths.FullPath);
            data.AddField("Version Number", item.Version.Number);
            data.AddField("ID", item.ID.Guid);
            data.AddField("Parent ID", item.ParentID.Guid);
            data.AddField("Template ID", item.TemplateID.Guid);

            return data;
        }

        private static List<object[]> GeFieldsProperties(Item item)
        {
            var result = new List<object[]>
                {
                    new object[] { "Section" , "Fields" }
                };

            item.Fields.ReadAll();

            var groupedFields = item.Fields
                .GroupBy(f => new { f.SectionSortorder, f.Section })
                .OrderBy(g => g.Key.SectionSortorder)
                .ThenBy(g => g.Key.Section);

            foreach (var group in groupedFields)
            {
                var results = new List<object[]>
                {
                    new object[] { "Field Title", "Field Type", "Value", "Contains Standard Value", "Unversioned", "Shared" }
                };

                results.AddRange(
                    group.OrderBy(f => f.Sortorder)
                        .Select(f =>
                            new object[] { !string.IsNullOrEmpty(f.Title) ? f.Title : f.DisplayName, f.Type, f.Value, f.ContainsStandardValue, f.Unversioned, f.Shared }));

                // TODO f.InheritsValueFromOtherItem not in 6.2
                 
                result.Add(new object[] 
                {
                    string.Format("{0}", group.Key.Section), results
                });
            }

            return result;
        }

        private static FieldList GetExtendedProperties(Item item)
        {
            var data = new FieldList();

            data.AddField("Key", item.Key);
            data.AddField("Has Children", item.HasChildren);

            if ((item.Children != null) && item.HasChildren)
            {
                data.AddField("Children", item.Children.Count);
            }

            if (item.Branch != null)
            {
                data.AddField("Branch Name", item.Branch.Name);
                data.AddField("Branch Id", item.BranchId.Guid);
            }

            data.AddField("Short Description", item.Appearance.ShortDescription);
            data.AddField("Long Description", item.Appearance.LongDescription);

            data.AddField("Originator Id", item.OriginatorId.Guid);
            data.AddField("Uri", item.Uri);
            data.AddField("Full Path", item.Paths.FullPath);
            data.AddField("Long ID", item.Paths.LongID);
            data.AddField("Hidden", item.Appearance.Hidden);
            data.AddField("Read Only", item.Appearance.ReadOnly);
            data.AddField("Sort order", item.Appearance.Sortorder);
            data.AddField("Style", item.Appearance.Style);

            // TODO not supported in 6.2
//            data.AddField("Is Clone", item.IsClone);
//            data.AddField("Is Item Clone", item.IsItemClone);
//            data.AddField("Source Uri", item.SourceUri);
            data.AddField("Created", item.Statistics.Created);
            data.AddField("Created By", item.Statistics.CreatedBy);
            data.AddField("Updated", item.Statistics.Updated);
            data.AddField("Updated By", item.Statistics.UpdatedBy);
            data.AddField("Revision", item.Statistics.Revision);

            return data;
        }
    }
}
