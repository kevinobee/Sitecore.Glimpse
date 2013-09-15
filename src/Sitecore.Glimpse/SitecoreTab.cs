using System;

using Glimpse.Core.Extensibility;
using Sitecore.Glimpse.Infrastructure;

namespace Sitecore.Glimpse
{
    public class SitecoreTab : TabBase, IDocumentation
    {
        private readonly ISitecoreRequest _sitecoreRequest;

        public SitecoreTab()
            : this(new SitecoreRequest())
        {
        }

        public SitecoreTab(ISitecoreRequest sitecoreRequest)
        {
            _sitecoreRequest = sitecoreRequest;
        }

        public override object GetData(ITabContext context)
        {
            try
            {
                var stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                return new object[] { 
                                new object[] { "Sitecore.Glimpse" },
                                new object[] {  _sitecoreRequest.GetData() },
                                new object[] {  string.Format("Execution Time: {0}ms", stopWatch.ElapsedMilliseconds) } 
                            };
            }
            catch (Exception ex)
            {
                return new object[] { 
                                new object[] { "Exception" },
                                new object[] { ex } 
                            };
            }
        }

        public override string Name
        {
            get { return "Sitecore"; }
        }

        public string DocumentationUri { get { return "https://github.com/kevinobee/Sitecore.Glimpse/wiki"; } }
    }
}