using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProducService.API.Models
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock {  get; set; }
    }
}
