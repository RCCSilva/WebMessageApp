using Gateway.Dto;
using Gateway.Services;
using KafkaLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gateway.Consumers
{
    public sealed class ReceiveMessageConsumer : KafkaConsumer<ChatMessage>
    {
        private readonly ReceiveMessageService _messageService;

        public ReceiveMessageConsumer(
            IConfiguration config, 
            ILogger<KafkaConsumer<ChatMessage>> logger, 
            ReceiveMessageService messageService) : base(config, logger)
        {
            _messageService = messageService;
            Topic = config.GetValue<string>("Kafka:SendMessageTopic");
        }

        protected override string Topic { get; set; }

        protected override void Handler(ChatMessage data)
        {
            _messageService.ReceiveMessage(data);
        }
    }
}