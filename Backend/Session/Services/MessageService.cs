using System.Linq;
using Confluent.Kafka;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;

namespace Session.Services
{
    public class MessageService
    {
        private readonly UserGatewayService _userGatewayService;
        private readonly KafkaProducer<Null, ChatMessage> _kafka;
        private readonly string _baseTopic;

        public MessageService(IConfiguration config, UserGatewayService userGatewayService,
            KafkaProducer<Null, ChatMessage> kafka)
        {
            _userGatewayService = userGatewayService;
            _kafka = kafka;
            _baseTopic = config.GetValue<string>("Kafka:ReceiveMessageTopic:Main");
        }

        public async void HandleMessage(Message<Null, ChatMessage> message)
        {
            var gateway = _userGatewayService.GetGateway(message.Value.ToUser);
            var topic = $"{_baseTopic}_{gateway}";
            await _kafka.ProduceAsync(topic, message);
        }
    }
}