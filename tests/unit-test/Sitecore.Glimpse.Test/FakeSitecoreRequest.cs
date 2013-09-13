namespace Sitecore.Glimpse.Test
{
    public class FakeSitecoreRequest : ISitecoreRequest
    {
        public RequestData GetData()
        {
            var data = new RequestData();
            data.Add(DataKey.Item, "foo-bar");
            return data;
        }
    }
}