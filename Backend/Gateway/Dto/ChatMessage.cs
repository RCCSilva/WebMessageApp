using Newtonsoft.Json;

namespace Gateway.Dto
{
    public class ChatMessage
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Message { get; set; }
    }
}