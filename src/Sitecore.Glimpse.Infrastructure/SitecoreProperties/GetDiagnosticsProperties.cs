using System.Collections.Generic;

namespace Sitecore.Glimpse.Infrastructure.SitecoreProperties
{
    public partial class SitecorePropertiesBusiness
    {
        public static List<object[]> GetDiagnosticsPropertiesFull(Sitecore.Diagnostics.DiagnosticContext diag)
        {
            var results = new List<object[]>()
                {
                    new object[] { "Diagnostics Property", "Value" },
                    new object[] { "Debugging", diag.Debugging},
                    new object[] { "Profiling", diag.Profiling},
                    new object[] { "Tracing", diag.Tracing},
                    new object[] { "Show Rendering Info", diag.ShowRenderingInfo},
                    new object[] { "Draw Rendering Borders", diag.DrawRenderingBorders}
                };
            return results;
        }

    }
}
