using Newtonsoft.Json;

namespace KafkaLibrary.Dto
{
    public class SessionCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}