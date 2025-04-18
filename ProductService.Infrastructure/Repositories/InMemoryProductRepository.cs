using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new()
        {
            new Product { Name = "Gaming Mouse", Description = "RGB Mouse", Price = 59.99m, Stock = 12},
            new Product { Name = "Mechanical Keyboard", Description = "Blue switches", Price = 99.99m, Stock = 7 }
        };


        public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult(_products.AsEnumerable());

        public Task<Product?> GetByIdAsync(Guid id) => 
            Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public Task AddAsync(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }
    }
}
