using CommandLine;

namespace Sitecore.Glimpse.Smoke.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (Parser.Default.ParseArguments(args, options))
            {
                TestRunner.Execute(options.Url);
            }
        }
    }
}
