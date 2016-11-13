using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

using Dustbuster.Models;
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

        // Method to 
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

                    //Memory leak here
                    //GetDBVersion();
                    
                    // Logic Error
                    //DbVersion = 0;
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
                    //old
                    //var EmulatedStorage = global::Android.OS.Environment.ExternalStorageDirectory.Path;

                    // new without the need for permissions R.L
                    var EmulatedStorage = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

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

                    Log.Debug("Android Log Test", "Database Downloaded!!!!!!!");
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

        public Task<Boolean> SendContactDetails(String contactInfo, DateTime requestDate, String contactName,
            String jobLocation, String contactType, String industryType)
        {
            

            UserInfoPHP userObject = new UserInfoPHP(contactInfo, requestDate, contactName, jobLocation, contactType, industryType);
            Console.WriteLine("userObject !!!: " + userObject.PrintObject());
            Uri uri = new Uri("http://www.rainstorm.com.au/app/Mailer/requestContact.php");
            HttpWebRequest  httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            httpWebRequest.ContentType = "text/html";
            httpWebRequest.Method = "POST";

            string jsonObj = JsonConvert.SerializeObject(userObject);

            Console.WriteLine("jsonObject!!!!: "+jsonObj);

            var data = Encoding.UTF8.GetBytes(jsonObj);
            httpWebRequest.ContentLength = data.Length;

           
            string webReturn;
            Boolean webReturnFlag;

            try
            {
                /*
                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                */

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonObj);
                    streamWriter.Flush();
                }


                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        webReturn = reader.ReadToEnd();
                        Console.WriteLine("PHP Web Response: {0}", webReturn);
                        reader.Close();
                    }
                }

                if (webReturn == "true")
                {
                    webReturnFlag = true;
                }
                else
                {
                    webReturnFlag = false;
                }

                return Task.FromResult(webReturnFlag);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Web Service Exception: {0}", ex);
                return Task.FromResult(false);
            }
        }

        public Task<Boolean> SendContactClient(String contactInfo, DateTime requestDate, String contactName,
            String jobLocation, String contactType, String industryType)
        {
            try
            {
                // serialise contact details into json
                UserInfoPHP userObject = new UserInfoPHP(contactInfo, requestDate, contactName, jobLocation, contactType, industryType);
                string jsonObj = JsonConvert.SerializeObject(userObject);
                // creating multipart form
                var multipart = new MultipartFormDataContent();
                var body = new StringContent(jsonObj, Encoding.UTF8, "multipart/form-data");
                multipart.Add(body);

                Console.WriteLine("Debug Multipart!!!: " + multipart);

                // creating the connection
                string resultContent;
                Boolean resultFlag;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://www.rainstorm.com.au/app/Mailer/requestContact.php");
                    var result = client.PostAsync(client.BaseAddress, multipart).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(resultContent);
                }

                if(resultContent == "true")
                {
                    resultFlag = true;
                }
                else
                {
                    resultFlag = false;
                }

                return Task.FromResult(resultFlag);


            }
            catch (Exception exception)
            {
                Console.WriteLine("Web Service Exception: {0}", exception);
                return Task.FromResult(false);
            }
        }

        public Task<Boolean> SendContactData(String contactInfo, DateTime requestDate, String contactName,
            String jobLocation, String contactType, String industryType)
        {

            string webReturn;
            Boolean webReturnFlag;
            String userObj = "contactInformation=" + contactInfo + "&requestedDate=" + requestDate+ "&contactsName=" + contactName + "&jobLocation=" + jobLocation + "&contactType="+contactType+"&industryToContact="+industryType;

            Uri uri = new Uri("http://www.rainstorm.com.au/app/Mailer/requestContact.php");
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";

            httpWebRequest.ContentLength = userObj.Length;

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(userObj);
                    streamWriter.Flush();
                }


                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        webReturn = reader.ReadToEnd();
                        Console.WriteLine("PHP Web Response: {0}", webReturn);
                        reader.Close();
                    }
                }

                if (webReturn == "true")
                {
                    webReturnFlag = true;
                }
                else
                {
                    webReturnFlag = false;
                }

                return Task.FromResult(webReturnFlag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Web Service Exception: {0}", ex);
                return Task.FromResult(false);
            }
        }



    }
}