using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;

namespace Gateway.Services
{
    public class SessionService
    {
        private readonly string _sessionTopic;
        private readonly KafkaProducer<Null, SessionCreate> _kafka;
        private readonly GuidService _guidService;

        public SessionService(
            KafkaProducer<Null, SessionCreate> kafka,
            IConfiguration config, GuidService guidService)
        {
            _kafka = kafka;
            _guidService = guidService;
            _sessionTopic = config.GetValue<string>("Kafka:SessionCreateTopic:Main");
        }

        public async Task SendConnection(SessionCreate sessionCreate)
        {
            var headers = new Headers {{"gateway-id", Encoding.ASCII.GetBytes(_guidService.GatewayId)}};

            var message = new Message<Null, SessionCreate>
            {
                Headers = headers,
                Value = sessionCreate
            };

            await _kafka.ProduceAsync(_sessionTopic, message);
        }
    }
}