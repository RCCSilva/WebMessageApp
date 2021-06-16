using System.Threading.Tasks;
using Confluent.Kafka;
using Gateway.Dtos;
using KafkaLibrary;
using Microsoft.Extensions.Configuration;

namespace Gateway.Services
{
    public class SessionService
    {
        private readonly string _sessionTopic;
        private readonly KafkaProducer<Null, ConnectRequestDto> _kafka;

        public SessionService(
            KafkaProducer<Null, ConnectRequestDto> kafka,
            IConfiguration config)
        {
            _kafka = kafka;
            _sessionTopic = config.GetValue<string>("Kafka:SessionCreateTopic");
        }

        public Task SendConnection(string name)
        {
            var message = new Message<Null, ConnectRequestDto>
            {
                Value = new ConnectRequestDto
                {
                    Name = name
                }
            };
            return _kafka.ProduceAsync(_sessionTopic, message);
        }
    }
}