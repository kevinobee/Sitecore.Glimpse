using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Glimpse.Infrastructure.Extensions;
using Sitecore.Glimpse.Model;
using Sitecore.Glimpse.Reflection;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class Controllers : ICollectionProvider<Controller>
    {
        private readonly ITypeProvider _typeProvider;
        private readonly ITypeProvider _servicesTypeProvider;

        private static readonly IDictionary<Type, ControllerType> TypeMapper = new Dictionary<Type, ControllerType>
        {
            { typeof(System.Web.Http.ApiController), ControllerType.WebAPI},
            { typeof(System.Web.Mvc.Controller), ControllerType.MVC}
        }; 

        public Controllers(ITypeProvider typeProvider, ITypeProvider servicesTypeProvider)
        {
            if (typeProvider == null) throw new ArgumentNullException("typeProvider");
            if (servicesTypeProvider == null) throw new ArgumentNullException("servicesTypeProvider");

            _typeProvider = typeProvider;
            _servicesTypeProvider = servicesTypeProvider;
        }

        public ICollection<Controller> Collection
        {
            get
            {
                var services = _servicesTypeProvider.Types.ToArray();

                var controllers = _typeProvider.Types
                                    .Where(x => ! x.IsAbstract)
                                    .Where(x => !services.Contains(x))
                                    .Where(x => GetControllerType(x) != null);

                return controllers.Select(x => new Controller(
                                                        x.FullName, 
                                                        GetControllerType(x).Value,
                                                        GetDefinition(x)))
                                   .ToArray();
            }
        }

        private static string GetDefinition(Type type)
        {
            return new TypeViewer(type, 
                                  TypeExtensions.IsRootType, 
                                  TypeExtensions.IsRootAttribute).ToJson();
        }

        private static ControllerType? GetControllerType(Type type)
        {
            foreach (var controllerType in TypeMapper.Keys)
            {
                if (IsSameOrSubclass(controllerType, type)) return TypeMapper[controllerType];
            }

            return null;
        }

        private static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase)
                   || potentialDescendant == potentialBase;
        }
    }
}