namespace Sitecore.Glimpse.Test
{
    public class FakeSitecoreRequest : ISitecoreRequest
    {
        public object GetData()
        {
            return new object[] { "foo-bar" };
        }
    }
}