using System;

namespace Sitecore.Glimpse
{
  public static class SystemTime
  {
    public static Func<DateTime> Now = () => DateTime.Now;
  }
}