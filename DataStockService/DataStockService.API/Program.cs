using Confluent.Kafka;
using DataStockService.API.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services
    .RegisterDatabase(builder.Configuration);

builder.Services.AddSingleton<IProducer<string, string>>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var producerConfig = new ProducerConfig
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
        Acks = Acks.All,
        EnableIdempotence = true,
        MessageSendMaxRetries = 3,
        RetryBackoffMs = 1000,
        LingerMs = 5,
        BatchSize = 16384
    };

    return new ProducerBuilder<string, string>(producerConfig)
        .SetLogHandler((_, logMessage) =>
        {
            var logger = serviceProvider.GetService<ILogger<IProducer<string, string>>>();
            var level = logMessage.Level switch
            {
                Confluent.Kafka.LogLevel.Error => LogLevel.Error,
                Confluent.Kafka.LogLevel.Warning => LogLevel.Warning,
                Confluent.Kafka.LogLevel.Info => LogLevel.Information,
                Confluent.Kafka.LogLevel.Debug => LogLevel.Debug,
                _ => LogLevel.Information
            };
            logger?.Log(level, "Kafka Producer: {Message}", logMessage.Message);
        })
        .Build();
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
