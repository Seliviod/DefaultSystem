using DataStockService.App.KafkaProducers;
using DataStockService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataStockService.App.BackgroundJobs
{
    public class OutboxSender(
        ILogger<OutboxSender> logger, 
        AppDbContext context,
        KafkaSimpleProducer kafkaSimpleProducer) 
        : BackgroundService
    {
        private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(5);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var startDate = DateTime.Now.AddDays(-7);
                    var endDate = DateTime.Now;

                    var events = await context.OutboxEvents
                        .Where(e => e.CreatedAt >= startDate && e.CreatedAt <= endDate)
                        .Where(e => e.Status == Core.Entities.OutboxEventStatus.New)
                        .ToListAsync();

                    foreach(var eve in events )
                    {
                        await kafkaSimpleProducer.Send(eve.Topic, eve.Payload, stoppingToken);
                    }

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }
    }
}
