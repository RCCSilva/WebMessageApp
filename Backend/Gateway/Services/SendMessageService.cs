using System.Threading.Tasks;
using Confluent.Kafka;
using Gateway.Dto;
using Gateway.Hubs;
using KafkaLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;

namespace Gateway.Services
{
    public class MessageService
    {
        private readonly string _messageTopic;
        private readonly KafkaProducer<Null, ChatMessage> _kafka;

        public MessageService(
            KafkaProducer<Null, ChatMessage> kafka,
            IConfiguration config)
        {
            _kafka = kafka;
            _messageTopic = config.GetValue<string>("Kafka:SendMessageTopic");
        }
        
        public Task SendMessage(ChatMessage chatMessage)
        {
            var message = new Message<Null, ChatMessage>
            {
                Value = chatMessage
            };

            return _kafka.ProduceAsync(_messageTopic, message);
        }
    }
}