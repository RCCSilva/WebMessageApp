using System;
using Confluent.Kafka;
using Gateway.Services;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gateway.Consumers
{
    public sealed class SessionReplyConsumer: KafkaConsumer<SessionCreate>
    {
        private readonly ILogger<SessionReplyConsumer> _logger;
        
        public SessionReplyConsumer(
            IConfiguration config, 
            ILogger<KafkaConsumer<SessionCreate>> logger,
            ILogger<SessionReplyConsumer> sessionLogger
            ) : base(config, logger)
        {
            Topic = config.GetValue<string>("Kafka:SessionCreateTopic:Reply");
            _logger = sessionLogger;
        }

        protected override string Topic { get; set; }
        protected override void Handler(Message<Null, SessionCreate> message)
        {
            _logger.LogDebug("Received data from " + message.Value.Name);
        }
    }
}