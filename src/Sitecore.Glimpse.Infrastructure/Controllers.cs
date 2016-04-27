using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Glimpse.Infrastructure.Extensions;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Sitecore.Glimpse.Model;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class Controllers : ICollectionProvider<Controller>
    {
        private static readonly IDictionary<Type, ControllerType> TypeMapper = new Dictionary<Type, ControllerType>
        {
            { typeof(System.Web.Http.ApiController), ControllerType.WebAPI },
            { typeof(System.Web.Mvc.Controller), ControllerType.MVC }
        };

        private readonly ITypeProvider _typeProvider;
        private readonly ITypeProvider _servicesTypeProvider;

        public Controllers(ITypeProvider typeProvider, ITypeProvider servicesTypeProvider)
        {
            if (typeProvider == null)
            {
                throw new ArgumentNullException("typeProvider");
            }

            if (servicesTypeProvider == null)
            {
                throw new ArgumentNullException("servicesTypeProvider");
            }

            _typeProvider = typeProvider;
            _servicesTypeProvider = servicesTypeProvider;
        }

        public ICollection<Controller> Collection
        {
            get
            {
                var services = _servicesTypeProvider.Types.ToArray();

                var controllers = _typeProvider.Types
                                    .Where(x => !x.IsAbstract)
                                    .Where(x => !services.Contains(x))
                                    .Select(x => new ControllerWrapper
                                    {
                                        Type = x, 
                                        ControllerType = GetControllerType(x)
                                    })
                                    .Where(x => x.ControllerType != null);

                return controllers.Select(x => BuildController(x))
                                  .ToArray();
            }
        }

        private static Controller BuildController(ControllerWrapper wrapper)
        {
            System.Diagnostics.Debug.Assert(
                wrapper.ControllerType.HasValue, 
                "ControllerWrapper.Controller must have a value");
            
            var typeViewer = new TypeViewer(wrapper.Type);

            return new Controller(
                wrapper.Type.FullName,
                wrapper.ControllerType.Value,
                typeViewer.ToJson(),
                typeViewer.CheckForMitigations(wrapper.ControllerType.Value),
                typeViewer.CheckForAuthorise(wrapper.ControllerType.Value));
        }

        private static ControllerType? GetControllerType(Type type)
        {
            foreach (var controllerType in TypeMapper.Keys
                                                     .Where(controllerType => IsSameOrSubclass(controllerType, type)))
            {
                return TypeMapper[controllerType];
            }

            return null;
        }

        private static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase)
                   || potentialDescendant == potentialBase;
        }
    }

    internal class ControllerWrapper
    {
        public ControllerType? ControllerType { get; set; }

        public Type Type { get; set; }
    }
}