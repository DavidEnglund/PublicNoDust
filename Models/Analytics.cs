using Plugin.GoogleAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster.Models
{
    class Analytics
    {
        public Analytics()
        {
            GoogleAnalytics.Current.Config.TrackingId = "***REMOVED***";
            GoogleAnalytics.Current.Config.AppId = "AppID";
            GoogleAnalytics.Current.Config.AppName = "dustbuster";
            GoogleAnalytics.Current.Config.AppInstallerId = Guid.NewGuid().ToString();
            //GoogleAnalytics.Current.Config.Debug = true;
        }
    }
}
