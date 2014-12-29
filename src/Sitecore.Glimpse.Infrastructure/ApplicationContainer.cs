using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using Sitecore.Glimpse.Infrastructure.Caching;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Glimpse.Model;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Configuration;
using Sitecore.Services.Core.Diagnostics;
using Sitecore.Services.Core.MetaData;
using Sitecore.Services.Infrastructure.Services;
using Sitecore.Services.Infrastructure.Sitecore.Configuration;
using Sitecore.Services.Infrastructure.Sitecore.Diagnostics;

namespace Sitecore.Glimpse.Infrastructure
{
    public static class ApplicationContainer
    {
        private static Assembly[] _siteAssemblies;

        public static IEnumerable<SitecoreService> SitecoreService()
        {
            var typeProvider = new ControllerAssemblyScanner();

            var nameGenerator = new NamespaceQualifiedUniqueNameGenerator(DefaultHttpControllerSelector.ControllerSuffix);

            IServicesConfiguration servicesConfiguration = new ServicesSettingsConfigurationProvider();

            var internalService = new SitecoreServices(
                                            typeProvider, 
                                            nameGenerator, 
                                            ResolveMetaDataBuilder(), 
                                            servicesConfiguration);

            return new CachingSitecoreServices(internalService, new WebCacheAdapter()).Collection;
        }

        public static IEnumerable<LoggedInUser> CurrentUsers()
        {
            return new CurrentUsers().Collection;
        }

        private static IMetaDataBuilder ResolveMetaDataBuilder()
        {
            var genericTypesToMapToArray = new List<string> { "List`1", "IEnumerable`1" };

            var entityParser = new EntityParser(
                  new JavascriptTypeMapper(),
                  genericTypesToMapToArray,
                  ResolveValidationMetaDataProvider());

            return new MetaDataBuilder(entityParser);
        }

        private static IValidationMetaDataProvider ResolveValidationMetaDataProvider()
        {
            var logger = ResolveLogger();

            return new AssemblyScannerValidationMetaDataProvider(
                            new ValidationMetaDataTypeProvider(GetSiteAssemblies()), logger);
        }

        public static ILogger ResolveLogger()
        {
            return new SitecoreLogger();
        }

        private static Assembly[] GetSiteAssemblies()
        {
            return _siteAssemblies ?? (_siteAssemblies = AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}