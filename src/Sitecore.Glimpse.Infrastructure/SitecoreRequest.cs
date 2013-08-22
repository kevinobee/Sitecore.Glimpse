using System;
using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreRequest : ISitecoreRequest
    {
        public object GetData()
        {
            var glimpseContext = new List<object[]>()
            {
                new object[] 
                {
                    "Sitecore Context Property", "Value" 
                }
            };

            AddSiteContext(glimpseContext);

            AddDatabaseContext(glimpseContext);

            AddItemContext(glimpseContext);

            AddItemTemplateContext(glimpseContext);

            AddItemVisualizationContext(glimpseContext);

            AddLanguageContext(glimpseContext);

            AddCultureContext(glimpseContext);

            AddDeviceContext(glimpseContext);

            AddDomainContext(glimpseContext);

            AddDiagnosticsContext(glimpseContext);

            // Sitecore.Context.Page
            // AddPageContext(glimpseContext);

            AddRequestContext(glimpseContext);

            AddUserContext(glimpseContext);

            // Sitecore.Context.Resources          
            
          
            return glimpseContext;
        }

        /// <summary>
        /// Add Sitecore.Context.Request
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddRequestContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Request", SitecoreProperties.SitecorePropertiesBusiness.GetRequestPropertiesFull(Sitecore.Context.Request)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Request Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.User
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddUserContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "User", SitecoreProperties.SitecorePropertiesBusiness.GetUserPropertiesFull(Sitecore.Context.User)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "User Plugin Exception", ex
                    });
            }
        }
       

        private static void AddPageContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Page", SitecoreProperties.SitecorePropertiesBusiness.GetPagePropertiesFull(Sitecore.Context.Page)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Page Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Diagnostics
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddDiagnosticsContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Diagnostics", SitecoreProperties.SitecorePropertiesBusiness.GetDiagnosticsPropertiesFull(Sitecore.Context.Diagnostics)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Diagnostics Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Domain
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddDomainContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Domain", SitecoreProperties.SitecorePropertiesBusiness.GetDomainPropertiesFull(Sitecore.Context.Domain)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Domain Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Item
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddItemContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Item", SitecoreProperties.SitecorePropertiesBusiness.GetItemPropertiesFull(Sitecore.Context.Item)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Item Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Item.Template
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddItemTemplateContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Item Template", SitecoreProperties.SitecorePropertiesBusiness.GetTemplatePropertiesFull(Sitecore.Context.Item.Template)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Item Template Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Item.Template
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddItemVisualizationContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Item Visualization", SitecoreProperties.SitecorePropertiesBusiness.GetVisualizationPropertiesFull(
                            Sitecore.Context.Item.Visualization, Sitecore.Context.Device)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Item Template Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Language
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddLanguageContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Language", SitecoreProperties.SitecorePropertiesBusiness.GetLanguagePropertiesFull(Sitecore.Context.Language)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Language Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Culture
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddCultureContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Culture", SitecoreProperties.SitecorePropertiesBusiness.GetCulturePropertiesFull(Sitecore.Context.Culture)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Culture Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Device
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddDeviceContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Device", SitecoreProperties.SitecorePropertiesBusiness.GetDevicePropertiesFull(Sitecore.Context.Device)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Device Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Database
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddDatabaseContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Database", SitecoreProperties.SitecorePropertiesBusiness.GetDatabaseProperties(Sitecore.Context.Database)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Database Plugin Exception", ex
                    });
            }
        }

        /// <summary>
        /// Add Sitecore.Context.Site
        /// </summary>
        /// <param name="glimpseContext"></param>
        private static void AddSiteContext(List<object[]> glimpseContext)
        {
            try
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Site",  SitecoreProperties.SitecorePropertiesBusiness.GetSiteProperties(Sitecore.Context.Site)
                    });
            }
            catch (Exception ex)
            {
                glimpseContext.Add(
                    new object[] 
                    { 
                        "Site Plugin Exception", ex
                    });
            }
        }

        
    }
}
