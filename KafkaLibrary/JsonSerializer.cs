using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace KafkaLibrary
{
    public class JsonSerializer<TV> : ISerializer<TV>
    {
        public byte[] Serialize(TV data, SerializationContext context)
        {
            var charArray = JsonConvert.SerializeObject(data).ToCharArray();
            return Encoding.GetEncoding("utf-8").GetBytes(charArray);
        }
    }
}