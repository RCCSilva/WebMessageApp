using System;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Gateway.Producer
{
    public class KafkaClientHandle: IDisposable
    {
        private readonly IProducer<byte[], byte[]> _kafkaProducer;

        public KafkaClientHandle(IConfiguration config)
        {
            var conf = new ProducerConfig();
            config.GetSection("Kafka:ProducerSettings").Bind(conf);
            _kafkaProducer = new ProducerBuilder<byte[], byte[]>(conf).Build();
        }

        public Handle Handle => _kafkaProducer.Handle;

        public void Dispose()
        {
            // Block until all outstanding produce requests have completed (with or
            // without error).
            _kafkaProducer.Flush();
            _kafkaProducer.Dispose();
        }
    }
}