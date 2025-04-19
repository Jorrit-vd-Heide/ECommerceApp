using EventBus.Events;

namespace ProductService.Application.Events
{
    public class ProductCreatedEvent : IntegrationEvent
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
