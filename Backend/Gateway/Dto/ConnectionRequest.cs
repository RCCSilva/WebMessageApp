using Newtonsoft.Json;

namespace Gateway.Dto
{
    public class ConnectionRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}