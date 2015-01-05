using System;
using Should;
using Sitecore.Glimpse.Infrastructure.Reflection;
using Xunit;

namespace Sitecore.Glimpse.Infrastructure.Test.Reflection
{
    public class AttributeViewerBehaviour
    {
        [Fact]
        public void trims_off_attribute_from_name()
        {
            var sut = new AttributeViewer(typeof (ObsoleteAttribute));

            sut.Name.ShouldEqual("Obsolete");
        }
    }
}