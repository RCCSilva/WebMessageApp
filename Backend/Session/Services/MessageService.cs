using System;
using Confluent.Kafka;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Session.Models;

namespace Session.Services
{
    public class MessageService
    {
        private readonly UserGatewayService _userGatewayService;
        private readonly KafkaProducer<Null, ChatMessage> _kafka;
        private readonly string _baseTopic;
        private readonly IServiceProvider _provider;

        public MessageService(IConfiguration config, UserGatewayService userGatewayService,
            KafkaProducer<Null, ChatMessage> kafka, IServiceProvider scope)
        {
            _userGatewayService = userGatewayService;
            _kafka = kafka;
            _provider = scope;
            _baseTopic = config.GetValue<string>("Kafka:ReceiveMessageTopic:Main");
        }

        public async void HandleMessage(Message<Null, ChatMessage> message)
        {
            var gateway = _userGatewayService.GetGateway(message.Value.ToUser);
            if (gateway == null)
            {
                await using var scope = _provider.CreateAsyncScope();
                var context = scope.ServiceProvider.GetService<SessionDbContext>();
                var chatMessage = JsonConvert.SerializeObject(message.Value);

                var storedMessage = new StoredMessage
                {
                    UserId = message.Value.ToUser,
                    ChatMessage = chatMessage
                };
                context?.StoredMessages.Add(storedMessage);
                await context?.SaveChangesAsync();
            }
            else
            {
                var topic = $"{_baseTopic}_{gateway}";
                await _kafka.ProduceAsync(topic, message);
            }
        }
    }
}