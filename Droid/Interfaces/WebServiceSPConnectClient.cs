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

using Dustbuster;
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

        // AddNewRecord to SP server
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

        /// <summary>
        /// GetDbVersion get the updated DB version from SP
        /// </summary>
        /// <returns>Task returns a boolean flag</returns>
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
                multipart.Add(new StringContent(contactInfo), String.Format("\"{0}\"", "contactInfomation"));
                multipart.Add(new StringContent(requestDate.ToString()), String.Format("\"{0}\"", "requestedDate"));
                multipart.Add(new StringContent(contactName), String.Format("\"{0}\"", "contactsName"));
                multipart.Add(new StringContent(jobLocation), String.Format("\"{0}\"", "jobLocation"));
                multipart.Add(new StringContent(contactType), String.Format("\"{0}\"", "contactType"));
                multipart.Add(new StringContent(industryType), String.Format("\"{0}\"", "industryToContact"));

                // creating the connection
                string resultContent;
                Boolean resultFlag;
                WebServiceResponseMsg responseModel;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://www.rainstorm.com.au/app/Mailer/requestContact.php");
                    var result = client.PostAsync(client.BaseAddress, multipart).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;

                    responseModel = JsonConvert.DeserializeObject<WebServiceResponseMsg>(resultContent);
                    
                }

                if(responseModel.success == true)
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
                return Task.FromResult(false);
            }
        }

        


    }
}