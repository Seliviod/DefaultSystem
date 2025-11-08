using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace DataStockService.App.KafkaProducers
{
    public class KafkaSimpleProducer //: IMessageProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<KafkaSimpleProducer> _logger;

        public KafkaSimpleProducer(IProducer<Null, string> producer, ILogger<KafkaSimpleProducer> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public async Task Send(string topic, string payload, CancellationToken cancellationToken)
        {
            var kafkaMessage = new Message<Null, string>
            {
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
