using Microsoft.AspNetCore.Mvc;
using ProducService.API.Models;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProducService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
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

            return CreatedAtAction(nameof(GetAll), new {id = product.Id}, product);
        }
    }
}
