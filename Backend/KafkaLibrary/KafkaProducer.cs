using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace KafkaLibrary
{
    public class KafkaProducer<TK, TV> where TV : class
    {
        private readonly IProducer<TK, TV> _kafkaHandle;
        private readonly ILogger<KafkaProducer<TK, TV>> _logger;

        public KafkaProducer(KafkaClientHandle handle, ILogger<KafkaProducer<TK, TV>> logger)
        {
            _logger = logger;
            _kafkaHandle = new DependentProducerBuilder<TK, TV>(handle.Handle)
                .SetValueSerializer(new JsonSerializer<TV>())
                .Build();
        }

        public Task ProduceAsync(string topic, Message<TK, TV> message)
        {
            _logger.LogDebug($"Sending message to '{topic}': {message}");
            return _kafkaHandle.ProduceAsync(topic, message);
        }

        public void Product(string topic, Message<TK, TV> message,
            Action<DeliveryReport<TK, TV>> deliveryHandler = null)
            => this._kafkaHandle.Produce(topic, message, deliveryHandler);
    }
}