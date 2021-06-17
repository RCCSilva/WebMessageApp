using System.Linq;
using Confluent.Kafka;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Session.Services;

namespace Session.Consumers
{
    public sealed class SendMessageConsumer: KafkaConsumer<ChatMessage>
    {
        private readonly UserGatewayService _userGatewayService;
        
        public SendMessageConsumer(
            IConfiguration config, 
            ILogger<KafkaConsumer<ChatMessage>> logger, 
            UserGatewayService userGatewayService) : base(config, logger)
        {
            _userGatewayService = userGatewayService;
            Topic = config.GetValue<string>("Kafka:SendMessageTopic:Main");
        }

        protected override string Topic { get; set; }
        protected override void Handler(Message<Null, ChatMessage> message)
        {
            
            throw new System.NotImplementedException();
        }
    }
}