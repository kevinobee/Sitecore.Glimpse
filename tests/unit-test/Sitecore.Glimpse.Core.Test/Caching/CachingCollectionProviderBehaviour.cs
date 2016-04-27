using Moq;
using Sitecore.Glimpse.Caching;
using Xunit;

namespace Sitecore.Glimpse.Core.Test.Caching
{
    public class CachingCollectionProviderBehaviour
    {
        private readonly Mock<ICollectionProvider<object>> _innerProvider;
        private readonly CachingCollectionProvider<object> _sut;
        private readonly Mock<ICache> _cache;
        private readonly string _fieldName;

        public CachingCollectionProviderBehaviour()
        {
            _fieldName = typeof(object).FullName;
            _innerProvider = new Mock<ICollectionProvider<object>>();
            _cache = new Mock<ICache>();

            _sut = new CachingCollectionProvider<object>(_innerProvider.Object, _cache.Object);  
        }

        [Fact]
        public void CallsInnerProviderToGetValues()
        {
            var data = _sut.Collection;

            _innerProvider.Verify(x => x.Collection, Times.Once);
        }

        [Fact]
        public void AddsInnerProviderValuesToCache()
        {
            var data = _sut.Collection;

            _cache.VerifySet(x => x[_fieldName] = It.IsAny<object>(), Times.Once);
        }
    }
}