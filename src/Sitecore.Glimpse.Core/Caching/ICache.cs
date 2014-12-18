namespace Sitecore.Glimpse.Caching
{
    public interface ICache
    {
        object this[string fieldName] { get; set; }
    }
}