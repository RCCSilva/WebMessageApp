using System;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Gateway.Consumer
{
    public class AppJsonDeserializer<TV>: IDeserializer<TV>
    {
        public TV Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var dataString = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<TV>(dataString);
        }
    }
}