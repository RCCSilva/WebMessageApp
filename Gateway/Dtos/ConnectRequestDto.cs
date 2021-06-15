using Newtonsoft.Json;

namespace Gateway.Dtos
{
    public class ConnectRequestDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}