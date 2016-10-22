using Plugin.GoogleAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{
    public class Analytics
    {
        public Analytics()
        {
            
        }

        public void SetDetails()
        {
			//Here we are setting the details that will allow us to connect to our google Analytics account
            GoogleAnalytics.Current.Config.TrackingId = "***REMOVED***";
            GoogleAnalytics.Current.Config.AppId = "AppID";
            GoogleAnalytics.Current.Config.AppName = "dustbuster";
            GoogleAnalytics.Current.Config.AppVersion = "1.0.0.0";
            //GoogleAnalytics.Current.Config.AppInstallerId = Guid.NewGuid().ToString();
            //GoogleAnalytics.Current.Config.Debug = true;
            //GoogleAnalytics.Current.Tracker.SendEvent("Category", "Action", "Labe;", 1);
        }

        public void SendAnalytics(string Company, string Traffic, string ResultTraffic, string Calendar, string ResultCalendar, string Rain, string ResultRain, string Location, string ResultLocation)
        {
            //Begin tracking information
            GoogleAnalytics.Current.InitTracker();
            //Send the page the user is on
            GoogleAnalytics.Current.Tracker.SendView("Maybe send users name instead of page?");
            //Send information about an event
            GoogleAnalytics.Current.Tracker.SendEvent(Company + " " + Traffic, ResultTraffic);
            GoogleAnalytics.Current.Tracker.SendEvent(Company + " " + Calendar, ResultCalendar);
            GoogleAnalytics.Current.Tracker.SendEvent(Company + " " + Rain, ResultRain);
            GoogleAnalytics.Current.Tracker.SendEvent(Company + " " + Location, ResultLocation);
        }
    }
}
