using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dustbuster
{
    public class HelpService
    {
        public async Task<Link[]> GetOnlineHelpLinks()
        {
            string page = "http://www.rainstorm.com.au/app/userHelp/getHelpLinks.php";  //target page
            string result;

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)      //gets the content of the page
            {
                result = await content.ReadAsStringAsync();     //reads the string
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                return JsonConvert.DeserializeObject<OnlineHelpLinks>(result).Links;    //uses Json.NET nugget to deserialize the Json
            }

            return null;
        }

    }
}
