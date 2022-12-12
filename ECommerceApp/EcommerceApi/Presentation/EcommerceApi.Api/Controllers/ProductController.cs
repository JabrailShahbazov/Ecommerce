using System.Net;
using Ecommerce.Application.Common.Concrates;
using Ecommerce.Application.Common.Consecrate;
using Ecommerce.Application.Repositories;
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

        public ProductController(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
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
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            Random r = new Random();
            foreach (var file in Request.Form.Files)
            {
                var fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.Name)}");

                await using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }

            return Ok();
        }
    }
}