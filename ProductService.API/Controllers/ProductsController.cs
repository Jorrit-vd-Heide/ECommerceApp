using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ProducService.API.Models;
using ProductService.Application.Events;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProducService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IEventBus _eventBus;

        public ProductsController(IProductRepository repo, IEventBus eventBus)
        {
            _repo = repo;
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _repo.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await _repo.AddAsync(product);

            var @event = new ProductCreatedEvent
            {
                Name = product.Name,
                Price = product.Price
            };

            _eventBus.Publish(@event);

            return CreatedAtAction(nameof(GetAll), new {id = product.Id}, product);
        }
    }
}
