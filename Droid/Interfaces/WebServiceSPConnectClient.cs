using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Dustbuster.Droid;

[assembly: Dependency(typeof(WebServiceSPConnectClient))]
namespace Dustbuster.Droid
{
    public class WebServiceSPConnectClient : IWebServiceConnect
    {
        au.com.sunhawk.Sunhawk_SP_svc sunhawk;


        // Method to test the connection between the app and the sharepoint server
        // returns a boolean that confirms or denies connection R.L
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
                    Console.WriteLine("Running database download!!!");
                    return Task.FromResult(result);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Exception: " + ex.Message);
                return Task.FromResult("Failed");

            }

        }

        public Task<Boolean> GetDB()
        {
            try
            {
                using (sunhawk = new au.com.sunhawk.Sunhawk_SP_svc())
                {
                    //getting path of external storage. only external storage permission were granted R.L
                    var EmulatedStorage = global::Android.OS.Environment.ExternalStorageDirectory.Path;
                    var FilePath = Path.Combine(EmulatedStorage, "fileTarget.txt");
                    // releasing resources for the file R.L
                    using (FileStream objfilestream = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        BinaryWriter BinWriter = new BinaryWriter(objfilestream);
                        long TotalBytes = BinWriter.BaseStream.Length;
                        Byte[] mybytearray = new Byte[TotalBytes];
                        mybytearray = sunhawk.getLatestDb();
                        // writing the file R.L
                        objfilestream.Write(mybytearray, 0, int.Parse(TotalBytes.ToString()));
                        objfilestream.Close();
                    }
                    Console.WriteLine("Database Downloaded!!!");
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Exception: " + ex.Message);
                return Task.FromResult(false);

            }
            finally
            {

            }
        }
    }
}