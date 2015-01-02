using System;
using Should;
using Sitecore.Glimpse.Reflection;
using Xunit;

namespace Sitecore.Glimpse.Core.Test.Reflection
{
    public class AttributeViewerBehaviour
    {
        [Fact]
        public void trims_off_attribute_from_name()
        {
            var sut = new AttributeViewer(typeof (ObsoleteAttribute), IsRootAttribute);

            sut.Name.ShouldEqual("Obsolete");
        }

        private bool IsRootAttribute(Type type)
        {
            throw new NotImplementedException();
        }
    }
}