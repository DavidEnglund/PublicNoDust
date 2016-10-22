using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Dustbuster.Droid.Client;
using Dustbuster.Interfaces;

[assembly: Dependency(typeof(WebServiceSPConnectClient))]
namespace Dustbuster.Droid.Client
{
    class WebServiceSPConnectClient : IWebServiceConnect
    {
        au.com.sunhawk.Sunhawk_SP_svc sunhawk;

        public Task<Boolean> TestConnection()
        {
            try
            {
                using (sunhawk = new au.com.sunhawk.Sunhawk_SP_svc())
                {
                    Boolean MethodResult;
                    Boolean SpecifiedResult;
                    sunhawk.testMethod(out MethodResult, out SpecifiedResult);
                    return Task.FromResult(MethodResult);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Exception: " + ex.Message);
                Boolean Result = false;
                return Task.FromResult(Result);

            }
        }

        public Task<Boolean> AddNewRecord(String Name, String Email, String Phone)
        {
            try
            {
                using (sunhawk = new au.com.sunhawk.Sunhawk_SP_svc())
                {
                    sunhawk.addContactAsync(Name, Email, Phone);

                    return Task.FromResult(true);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Exception: " + ex.Message);
                return Task.FromResult(false);

            }
        }

        public Task<String> GetDBVersion()
        {
            try
            {
                using (sunhawk = new au.com.sunhawk.Sunhawk_SP_svc())
                {
                    int DbVersion;
                    bool LatestDBVersion;
                    sunhawk.getLatestDbVersion(out DbVersion, out LatestDBVersion);
                    var result = DbVersion + " " + LatestDBVersion;
                    return Task.FromResult(result);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Exception: " + ex.Message);
                return Task.FromResult("Failed");

            }
        }
    }
}