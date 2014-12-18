using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Glimpse.Extensions;
using Sitecore.Glimpse.Model;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Configuration;
using Sitecore.Services.Infrastructure.Services;

namespace Sitecore.Glimpse.Infrastructure
{
    public class SitecoreServices : ICollectionProvider<SitecoreService>  
    {
        private readonly ITypeProvider _typeProvider;
        private readonly IControllerNameGenerator _controllerNameGenerator;
        private readonly IMetaDataBuilder _metaDataBuilder;
        private readonly IServicesConfiguration _servicesConfiguration;

        public SitecoreServices(ITypeProvider typeProvider, 
                                IControllerNameGenerator controllerNameGenerator, 
                                IMetaDataBuilder metaDataBuilder, 
                                IServicesConfiguration servicesConfiguration) 
        {
            if (typeProvider == null) throw new ArgumentNullException("typeProvider");
            if (controllerNameGenerator == null) throw new ArgumentNullException("controllerNameGenerator");
            if (metaDataBuilder == null) throw new ArgumentNullException("metaDataBuilder");
            if (servicesConfiguration == null) throw new ArgumentNullException("servicesConfiguration");

            _typeProvider = typeProvider;
            _controllerNameGenerator = controllerNameGenerator;
            _metaDataBuilder = metaDataBuilder;
            _servicesConfiguration = servicesConfiguration;
        }

        public ICollection<SitecoreService> Collection
        {
            get { return _typeProvider.Types.Select(BuildSitecoreService).Where(x => x != null).ToArray(); }
        }

        private SitecoreService BuildSitecoreService(Type controllerType)
        {
            var service = new SitecoreService
            {
                Controller = RemoveControllerSuffix(controllerType.FullName),
                Url = GetRouteFromType(controllerType),
                Attributes = controllerType.GetAttributes().ToArray()
            };

            var entityService = controllerType.GetGenericInterface(typeof(IEntityService<>));
            
            if (entityService != null)
            {
                var pocoObject = entityService.GetGenericArguments()[0];

                service.IsEntityService = true;
                service.ObjectType = pocoObject.FullName;
                service.Metadata = GetMetadata(pocoObject);
            }

            return service;
        }

        private string GetMetadata(Type type)
        {
            try
            {
                return _metaDataBuilder.Parse(type);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        private string GetRouteFromType(Type controllerType)
        {
            var name = _controllerNameGenerator.GetName(controllerType);

            return string.Concat(_servicesConfiguration.Configuration.Services.Routes.RouteBase, name.Replace('.', '/'));
        }

        private static string RemoveControllerSuffix(string name)
        {
            return name.Remove(name.Length - "Controller".Length);
        }
    }
}