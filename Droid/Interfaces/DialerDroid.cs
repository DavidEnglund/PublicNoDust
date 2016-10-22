using System;

using Android.App;
using Android.Content;

using Xamarin.Forms;
using Dustbuster.Droid;

[assembly: Dependency(typeof(DialerDroid))]
namespace Dustbuster.Droid
{
    public class DialerDroid : IDialer
    {
        //make sure dial permissions are set in manifest
        public void Dial(string number)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", number));
                var intent = new Intent(Intent.ActionCall, uri);
                Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {

                new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("OK", (sender, args) =>
                {
                    //action when ok pressed
                }).SetMessage(ex.ToString())
                .SetTitle("Android Exception")
                .Show();
            }
        }
    }
}