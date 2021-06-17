using System;
using Gateway.Dto;
using Gateway.Services;
using KafkaLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gateway.Consumers
{
    public sealed class SessionReplyConsumer: KafkaConsumer<ConnectionRequest>
    {
        private readonly ILogger<SessionReplyConsumer> _logger;
        
        public SessionReplyConsumer(
            IConfiguration config, 
            ILogger<KafkaConsumer<ConnectionRequest>> logger,
            ILogger<SessionReplyConsumer> sessionLogger
            ) : base(config, logger)
        {
            Topic = config.GetValue<string>("Kafka:SessionCreateTopic");
            _logger = sessionLogger;
        }

        protected override string Topic { get; set; }
        protected override void Handler(ConnectionRequest data)
        {
            _logger.LogDebug("Received data from " + data);
        }
    }
}