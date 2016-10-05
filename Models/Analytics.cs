using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.GoogleAnalytics;

namespace Dustbuster.Models
{
    class Analytics
    {
        public Analytics()
        {

        }

        public void SetUp()
        {
            GoogleAnalytics.Current.Config.TrackingId = "UA-11111111-1";
            GoogleAnalytics.Current.Config.AppId = "AppID";
            GoogleAnalytics.Current.Config.AppName = "TEST";
            GoogleAnalytics.Current.Config.AppVersion = "1.0.0.0";
        }
    }
}
