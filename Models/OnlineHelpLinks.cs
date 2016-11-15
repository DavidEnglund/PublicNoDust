using Newtonsoft.Json;

namespace Dustbuster
{
    public class OnlineHelpLinks
    {
        public Link[] Links;
    }

    public class Link
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("link")]
        public string URL { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
