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
        public async Task Get()
        {
          var products = await _productReadRepository.GetByIdAsync("7132799d-0e91-4ac2-b6c9-7f205ce8ada4");

          if (products != null) products.Name = "Menimsen";

          await _productWriteRepository.SaveAsync();
        }
    }
}
