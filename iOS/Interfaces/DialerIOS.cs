using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dustbuster.Interfaces;
using Dustbuster.iOS.Interfaces;
using Xamarin.Forms;
using UIKit;
using Foundation;

[assembly: Dependency(typeof(DialerIOS))]
namespace Dustbuster.iOS.Interfaces
{
    class DialerIOS : IDialer
    {
        public void Dial(string number)
        {
            // first setup and uri for iOS to use with the given phone number
            Uri phoneCall = new NSUrl("tel:" + number);
            // then try to call it showing a popup message if the call cannot be made ( I think this will only show if the device does not even have call ability - sim,ipad, ect..)
            if (!UIApplication.SharedApplication.OpenUrl(phoneCall))
            {
                var errorMsg = new UIAlertView("Not supported",
                  "This device cannot make a phone call at this time",
                  null,
                  "OK",
                  null);
                errorMsg.Show();
            };
        }
    }
}