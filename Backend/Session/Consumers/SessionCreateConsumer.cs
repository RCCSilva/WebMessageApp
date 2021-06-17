using System.Linq;
using System.Text;
using Confluent.Kafka;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Session.Services;

namespace Session.Consumers
{
    public sealed class SessionCreateConsumer: KafkaConsumer<SessionCreate>
    {
        private readonly UserGatewayService _userGatewayService;
        
        public SessionCreateConsumer(
            IConfiguration config, 
            ILogger<KafkaConsumer<SessionCreate>> logger, 
            UserGatewayService userGatewayService) : base(config, logger)
        {
            _userGatewayService = userGatewayService;
            Topic = config.GetValue<string>("Kafka:SessionCreateTopic:Main");
        }

        protected override string Topic { get; set; }
        protected override void Handler(Message<Null, SessionCreate> message)
        {
            var gatewayIdByte = message.Headers.First(h => h.Key == "gateway-id").GetValueBytes();
            var gatewayId= Encoding.ASCII.GetString(gatewayIdByte);

            _userGatewayService.AddUser(message.Value.Name, gatewayId);
        }
    }
}