using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Gateway.Hubs;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;

namespace Gateway.Services
{
    public class MessageService
    {
        private readonly string _messageTopic;
        private readonly KafkaProducer<Null, ChatMessage> _kafka;
        private readonly GuidService _guidService;

        public MessageService(
            KafkaProducer<Null, ChatMessage> kafka,
            IConfiguration config,
            GuidService guidService)
        {
            _kafka = kafka;
            _guidService = guidService;
            _messageTopic = config.GetValue<string>("Kafka:SendMessageTopic:Main");
        }

        public Task SendMessage(ChatMessage chatMessage)
        {
            var headers = new Headers {{"gateway-id", Encoding.ASCII.GetBytes(_guidService.GatewayId)}};

            var message = new Message<Null, ChatMessage>
            {
                Headers = headers,
                Value = chatMessage
            };

            return _kafka.ProduceAsync(_messageTopic, message);
        }
    }
}