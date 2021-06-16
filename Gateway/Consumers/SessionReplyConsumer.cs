using System;
using Gateway.Dtos;
using Gateway.Services;
using KafkaLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gateway.Consumers
{
    public sealed class SessionReplyConsumer: KafkaConsumer<ConnectRequestDto>
    {
        private ILogger<SessionReplyConsumer> _logger;
        
        public SessionReplyConsumer(
            IConfiguration config, 
            ILogger<KafkaConsumer<ConnectRequestDto>> logger,
            ILogger<SessionReplyConsumer> sessionLogger
            ) : base(config, logger)
        {
            Topic = config.GetValue<string>("Kafka:SessionCreateTopic");
            _logger = sessionLogger;
        }

        protected override string Topic { get; set; }
        protected override void Handler(ConnectRequestDto data)
        {
            _logger.LogDebug("Received data from " + data);
        }
    }
}