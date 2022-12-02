using Ecommerce.Application.Repositories;
using EcommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = _productReadRepository.GetAll();

            return Ok(products);
        }
    }
}