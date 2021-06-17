using System.Threading.Tasks;
using Confluent.Kafka;
using Gateway.Dto;
using KafkaLibrary;
using Microsoft.Extensions.Configuration;

namespace Gateway.Services
{
    public class SessionService
    {
        private readonly string _sessionTopic;
        private readonly KafkaProducer<Null, ConnectionRequest> _kafka;

        public SessionService(
            KafkaProducer<Null, ConnectionRequest> kafka,
            IConfiguration config)
        {
            _kafka = kafka;
            _sessionTopic = config.GetValue<string>("Kafka:SessionCreateTopic");
        }

        public Task SendConnection(ConnectionRequest connectionRequest)
        {
            var message = new Message<Null, ConnectionRequest>
            {
                Value = connectionRequest
            };
            return _kafka.ProduceAsync(_sessionTopic, message);
        }
    }
}