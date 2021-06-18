using Confluent.Kafka;
using Gateway.Consumers;
using Gateway.Services;
using KafkaLibrary;
using KafkaLibrary.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway
{
    public static class ContainerHandler
    {
        public static void AddContainers(IServiceCollection services)
        {
            services.AddSingleton<GuidService>();
            
            services.AddSingleton<KafkaClientHandle>();
            services.AddSingleton<UserConnectionService>();

            services.AddSingleton<ReceiveMessageService>();

            services.AddScoped<KafkaProducer<Null, SessionCreate>>();
            services.AddScoped<KafkaProducer<Null, ChatMessage>>();

            services.AddScoped<SessionService>();
            services.AddScoped<MessageService>();

            services.AddHostedService<SessionReplyConsumer>();
            services.AddHostedService<ReceiveMessageConsumer>();
        }
    }
}