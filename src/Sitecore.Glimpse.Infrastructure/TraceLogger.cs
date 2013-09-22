namespace Sitecore.Glimpse.Infrastructure
{
    public class TraceLogger : ILog
    {
        public void Write(string message)
        {
            System.Diagnostics.Trace.Write(message);
        }
    }
}