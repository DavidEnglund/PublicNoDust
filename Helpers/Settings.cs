using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Dustbuster.Helpers
{
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    private const string EnableAnalyticsKey = "enable_analytics";
	private static readonly bool EnableAnalyticsDefault = true;

    public static bool EnableAnalytics
    {
      get
      {
        return AppSettings.GetValueOrDefault(EnableAnalyticsKey, EnableAnalyticsDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue(EnableAnalyticsKey, value);
      }
    }

  }
}