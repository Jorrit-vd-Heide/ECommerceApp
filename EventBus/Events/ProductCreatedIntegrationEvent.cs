namespace EventBus.Events
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    { 
        public Guid ProductID { get; set; }
        public string Name { get; set; }

        public ProductCreatedIntegrationEvent(Guid productId, string name) 
        {
            ProductID = productId;
            Name = name;
        }
    }
}
