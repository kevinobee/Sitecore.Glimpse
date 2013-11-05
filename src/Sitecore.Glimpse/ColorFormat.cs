namespace Sitecore.Glimpse
{
    internal class ColorFormat
    {
        public const string Highlight = "blue";
        public const string Lowlight = "grey";

        public static string Colorize(string colour, string data)
        {
            return string.Format("<span style=\"colour:{0};\">{1}</span>", colour, data);
        }
    }
}