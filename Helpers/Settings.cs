using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Dustbuster
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


		private const string CustomerNameKey = "customer_name";
		private static readonly string CustomerNameDefault = "";

		private const string ContactMethodKey = "contact_method";
		private static readonly int ContactMethodDefault = 1;

		private const string ContactInfoKey = "contact_info";
		private static readonly string ContactInfoDefault = "";

		private const string EnableAnalyticsKey = "enable_analytics";
		private static readonly bool EnableAnalyticsDefault = true;

		private const string EnableReadModeKey = "enable_readmode";
		private static readonly bool EnableReadModeDefault = false;

		public static string CustomerName
		{
			get
			{
				return AppSettings.GetValueOrDefault(CustomerNameKey, CustomerNameDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(CustomerNameKey, value);
			}
		}

		public static int ContactMethod
		{
			get
			{
				return AppSettings.GetValueOrDefault(ContactMethodKey, ContactMethodDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(ContactMethodKey, value);
			}
		}

		public static string ContactInfo
		{
			get
			{
				return AppSettings.GetValueOrDefault(ContactInfoKey, ContactInfoDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(ContactInfoKey, value);
			}
		}

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

		public static bool EnableReadMode
        {
            get
            {
                return AppSettings.GetValueOrDefault(EnableReadModeKey, EnableReadModeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(EnableReadModeKey, value);

                App.Current.Resources["labelStyle"] = (value) ? App.Current.Resources["readModeLabelStyle"] : App.Current.Resources["normalLabelStyle"];
            }
        }
    }
}
