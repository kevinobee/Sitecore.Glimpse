using System;
using System.Net.Http;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Model;
using Sitecore.Services.Infrastructure.Services;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Glimpse.Infrastructure.Test
{
    [ServicesController]
    public class TestController : ServicesApiController
    {
    }

    public class NonServicesTestController : ServicesApiController
    {
    }

    public class TestService : IEntityService<EntityIdentity>
    {
        public EntityIdentity[] FetchEntities()
        {
            throw new NotImplementedException();
        }

        public EntityIdentity FetchEntity(string id)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage CreateEntity(EntityIdentity entity)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage UpdateEntity(EntityIdentity entity)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Delete(EntityIdentity entity)
        {
            throw new NotImplementedException();
        }
    }

    public class MvcController : System.Web.Mvc.Controller
    {
    }

    public class WebApiController : System.Web.Http.ApiController
    {
    }
}