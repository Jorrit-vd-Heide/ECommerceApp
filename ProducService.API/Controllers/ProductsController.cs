using Microsoft.AspNetCore.Mvc;
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
    }
}
