using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace DataStockService.App.KafkaProducers
{
    public class KafkaPartitionProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaPartitionProducer> _logger;

        public KafkaPartitionProducer(IProducer<string, string> producer, ILogger<KafkaPartitionProducer> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public async Task Send(string key, string topic, string payload, CancellationToken cancellationToken)
        {
            var kafkaMessage = new Message<string, string>
            {
                Key = key,
                Value = payload
            };

            var produceResult = await _producer.ProduceAsync(topic, kafkaMessage, cancellationToken);

            if (produceResult.Status == PersistenceStatus.Persisted)
            {
                //await MarkMessageAsCompletedAsync(dbConnection, message.Id);

                _logger.LogInformation($"Kafka produced, message: {produceResult.Message}");
            }
            else
            {
                _logger.LogError("some error");
            }
        }
    }
}
