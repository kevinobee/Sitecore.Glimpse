using System;
using System.Web.Http;
using Should;
using Sitecore.Glimpse.Infrastructure.Extensions;
using Sitecore.Services.Infrastructure.Services;
using Sitecore.Services.Infrastructure.Web.Http;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Test.Extensions
{
    public class IsRootTypeBehaviour
    {
        [Theory]
        [InlineData(typeof(ServicesApiController), true)]
        [InlineData(typeof(ApiController), true)]
        [InlineData(typeof(EntityServiceBase<>), true)]
        public void Root_type_checks(Type type, bool isRoot)
        {
            TypeExtensions.IsRootType(type).ShouldEqual(isRoot);
        }
    }
}