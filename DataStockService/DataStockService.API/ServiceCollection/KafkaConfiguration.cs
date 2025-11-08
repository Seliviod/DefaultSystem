using Confluent.Kafka;

namespace DataStockService.API.ServiceCollection
{
    public static class KafkaConfiguration
    {
        public static IServiceCollection AddKafkaProducers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProducer<string, string>>(serviceProvider =>
            {

                var producerConfig = new ProducerConfig
                {
                    BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
                    Acks = Acks.All,
                    EnableIdempotence = true,
                    MessageSendMaxRetries = 3,
                    RetryBackoffMs = 1000,
                    LingerMs = 5,
                    BatchSize = 16384 // Размер батча перед реальной отправкой сообщения


                    //BootstrapServers = "kafka1:9092,kafka2:9092,kafka3:9092",

                    //// Надежность
                    //Acks = Acks.All,
                    //EnableIdempotence = true,
                    //MessageSendMaxRetries = 5,
                    //RetryBackoffMs = 1000,

                    //// Производительность
                    //LingerMs = 10,
                    //BatchSize = 32768,           // 32 KB
                    //CompressionType = CompressionType.Lz4,
                    //BatchNumMessages = 1000,     // Макс сообщений в батче

                    //// Сетевые настройки
                    //RequestTimeoutMs = 30000,
                    //SocketTimeoutMs = 60000,
                    //SocketKeepaliveEnable = true,

                    //// Безопасность
                    //SecurityProtocol = SecurityProtocol.SaslSsl,
                    //SaslMechanism = SaslMechanism.Plain,
                    //SaslUsername = "username",
                    //SaslPassword = "password",
                    //SslCaLocation = "/path/to/ca.crt",

                    //// Мониторинг
                    //StatisticsIntervalMs = 5000,  // Сбор статистики каждые 5 сек
                    //EnableBackgroundPoll = true   // Фоновый polling delivery reports
                };

                return new ProducerBuilder<string, string>(producerConfig).Build();
            });

            services.AddSingleton<IProducer<Null, string>>(serviceProvider =>
            {

                var producerConfig = new ProducerConfig
                {
                    BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
                    Acks = Acks.All,
                    EnableIdempotence = true,
                    MessageSendMaxRetries = 3,
                    RetryBackoffMs = 1000,
                    LingerMs = 5,
                    BatchSize = 16384 // Размер батча перед реальной отправкой сообщения
                };

                return new ProducerBuilder<Null, string>(producerConfig).Build();
            });

            return services;
        }
    }
}
