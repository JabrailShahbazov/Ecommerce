using System.Net;
using Ecommerce.Application.Common.Concrates;
using Ecommerce.Application.Common.Consecrate;
using Ecommerce.Application.Repositories;
using Ecommerce.Application.Services;
using Ecommerce.Application.ViewModels.Product;
using EcommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public ProductController(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationDto pagination)
        {
            var productCount = await _productReadRepository.GetCount(false);

            var listProducts = await _productReadRepository.GetAll(false).Select(p => new ProductListDto()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Stock = p.Stock,
                Price = p.Price,
                CreateDate = p.CreateDate,
                UpdatedDate = p.UpdatedDate
            }).OrderByDescending(o => o.CreateDate).Skip(pagination.Size * pagination.Page).Take(pagination.Size).ToListAsync();
            var a = new PagedResultDto<ProductListDto>()
            {
                TotalCount = productCount,
                Items = listProducts
            };
            return Ok(a);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductVM model)
        {
            await _productWriteRepository.AddAsync(new Product()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price
            });

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductVM model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id);

            if (product != null)
            {
                product.Stock = model.Stock;
                product.Name = model.Name;
                product.Price = model.Price;
            }

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
           await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();
        }
    }
}