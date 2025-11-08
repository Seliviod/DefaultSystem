namespace DataStockService.Core.Entities
{
    public class OutboxEvent
    {
        public OutboxEvent(string topic, string key, string entityName, string payload)
        {
            CreatedAt = DateTime.UtcNow;
            Topic = topic;
            Key = key;
            EntityName = entityName;
            Payload = payload;
            Status = OutboxEventStatus.New;
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Topic { get; set; }
        public string Key { get; set; }
        public string EntityName { get; set; }
        public string Payload { get; set; } // Может стоит сделать object и будем парсить к нужному типу в consumer
        public OutboxEventStatus Status { get; set; }
    }

    public enum OutboxEventStatus
    {
        None = 0,
        New,
        Success,
        Failure
    }
}
