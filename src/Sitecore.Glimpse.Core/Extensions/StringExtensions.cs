namespace Sitecore.Glimpse.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveFromEnd(this string s, string suffix)
        {
            return s.EndsWith(suffix) ? s.Substring(0, s.Length - suffix.Length) : s;
        }
    }
}