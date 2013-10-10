using CommandLine;
using CommandLine.Text;

namespace Sitecore.Glimpse.Smoke.Test
{
    internal class Options
    {
        [Option('u', "url", Required = false, DefaultValue = "http://glimpsetest/", HelpText = "Website url to run tests against")]
        public string Url { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                                      current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}