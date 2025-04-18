namespace EventBus.Events
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}
