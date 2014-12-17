using System;
using Sitecore.Glimpse.Model;
using Sitecore.Services.Core;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class SitecoreServices
    {
        public SitecoreServices(IControllerNameGenerator controllerNameGenerator)
        {
            // TODO
        }

        public SitecoreService[] GetServices()
        {
            return new[] { new SitecoreService
            {
                ClassName = "ItemService", 
                Route = "/sitecore/api/ssc/item"
            } };

        }
    }
}