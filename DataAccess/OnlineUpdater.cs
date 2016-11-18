using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster.DataAccess
{
    // this class will not need to made just run so static shoudl be fine
    // this will contain methods to update the database from the online source at app.rainstorm.com.au
    /// <summary>
    /// this static class contains methods to update the local database from an online one.
    /// </summary>
    static class OnlineUpdater
    {
        
        // this is the connecction method that already has the 1st part of the webaddress in it and just needs the page(might change this to have the page as the name of the table).
        private static async Task<string> connection(string webpage)
        {
            string page = "http://www.rainstorm.com.au/app/Update/" + webpage + ".php";  //target page
            string result = "";
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)      //gets the content of the page
                {
                    result = await content.ReadAsStringAsync();     //reads the string
                }
            }
            catch (WebException webError)
            {
                Debug.WriteLine(webError.ToString());
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.ToString());
            }
                return result;
        }

        // this method will return true if the online database is a newer version number than the local one
        public static async Task<bool> UpdateAvailable()
        {
            string page = "DBMetaData";
            string result = await connection(page);
            List<DBMetaData> onlineMetaData = JsonConvert.DeserializeObject<List<DBMetaData>>(result);
            int onlineVersionNumber = onlineMetaData[0].DBVersion;
            if (onlineVersionNumber > App.ProductsDb.GetDBVersion())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> UpdateProductMatrix()
        {
            // this will update the productmatrix table form online. 1st it give the connection the page to connect to then gets the json string back from the connection.
            // if its not null it will create a list from the json and then run the update db method.
            string page = "ProductMatrix";  
            string result = await connection(page);
            List<ProductMatrix> productMatrixFromDB;

            if (!string.IsNullOrWhiteSpace(result))
            {
               productMatrixFromDB =  JsonConvert.DeserializeObject<List<ProductMatrix>>(result);    //uses Json.NET nugget to deserialize the Json
                App.ProductsDb.UpdateProductMatrix(productMatrixFromDB);
                return true;
            }

            return false;
            
        }
    }

}
