using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using Should;
using Should.Core.Exceptions;
using Sitecore.Glimpse.Infrastructure.Extensions;
using Xunit.Extensions;

namespace Sitecore.Glimpse.Infrastructure.Test.Extensions
{
    public class IsRootAttributeBehaviour
    {
        [Theory]
        [InlineData(typeof(Attribute), true)]
        [InlineData(typeof(FilterAttribute), true)]
        [InlineData(typeof(ActionFilterAttribute), true)]
        [InlineData(typeof(ActionNameAttribute), true)]
        [InlineData(typeof(EnableCorsAttribute), false)]
        [InlineData(typeof(AuthorizationFilterAttribute), true)]
        [InlineData(typeof(System.Web.Mvc.ActionFilterAttribute), true)]
        [InlineData(typeof(System.Web.Mvc.AcceptVerbsAttribute), true)]
        [InlineData(typeof(System.Web.Mvc.FilterAttribute), true)]
        [InlineData(typeof(System.Web.Mvc.ActionMethodSelectorAttribute), true)]
        public void Root_attribute_checks(Type attributeType, bool isRoot)
        {
            TypeExtensions.IsRootAttribute(attributeType).ShouldEqual(isRoot);
        }
    }
}