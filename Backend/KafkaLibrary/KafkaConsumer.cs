using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaLibrary
{
    public abstract class KafkaConsumer<TV> : BackgroundService
    {
        protected abstract string Topic { get; set; }
        protected abstract void Handler(Message<Null, TV> message);

        private readonly ILogger<KafkaConsumer<TV>> _logger;
        private readonly IConsumer<Null, TV> _kafkaConsumer;

        protected KafkaConsumer(IConfiguration config, ILogger<KafkaConsumer<TV>> logger)
        {
            _logger = logger;
            var consumerConfig = new ConsumerConfig();
            config.GetSection("Kafka:ConsumerSettings").Bind(consumerConfig);

            _kafkaConsumer = new ConsumerBuilder<Null, TV>(consumerConfig)
                .SetValueDeserializer(new AppJsonDeserializer<TV>())
                .Build();
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            new Thread(() => StartConsumerLoop(cancellationToken)).Start();

            return Task.CompletedTask;
        }

        private void StartConsumerLoop(CancellationToken cancellationToken)
        {
            _kafkaConsumer.Subscribe(Topic);
            _logger.LogInformation($"Consumer subscribed to '{Topic}'");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _kafkaConsumer.Consume(cancellationToken);

                    _logger.LogInformation($"Received message {cr.Message.Key}: {cr.Message.Value}");
                    Handler(cr.Message);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    _logger.LogError($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Unexpected error: {e}");
                    break;
                }
            }
        }

        public override void Dispose()
        {
            _kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            _kafkaConsumer.Dispose();

            base.Dispose();
        }
    }
}