using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Dustbuster
{
    public class SendContactsClient
    {
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

                if (responseModel.success == true)
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
