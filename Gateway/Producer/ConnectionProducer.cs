using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Gateway.Producer
{
    public class KafkaDependentProducer<TK, TV> where TV : class
    {
        
        private readonly IProducer<TK, TV> _kafkaHandle;

        public KafkaDependentProducer(KafkaClientHandle handle)
        {
            _kafkaHandle = new DependentProducerBuilder<TK, TV>(handle.Handle)
                .SetValueSerializer(new JsonSerializer<TV>())
                .Build();
        }

        public Task ProduceAsync(string topic, Message<TK, TV> message)
            => this._kafkaHandle.ProduceAsync(topic, message);

        public void Product(string topic, Message<TK, TV> message,
            Action<DeliveryReport<TK, TV>> deliveryHandler = null)
            => this._kafkaHandle.Produce(topic, message, deliveryHandler);
    }
}