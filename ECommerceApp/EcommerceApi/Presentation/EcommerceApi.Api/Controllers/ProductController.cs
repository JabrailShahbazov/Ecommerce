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
        private readonly IFileService _fileService;

        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;

        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        public ProductController(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IFileService fileService,
            IFileWriteRepository fileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IFileReadRepository fileReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _fileReadRepository = fileReadRepository;
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
            var datas = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
           await _productImageFileWriteRepository.AddManyAsync(datas.Select(d => new ProductImageFile()
           {
               FileName = d.fileName,
               Path = d.path
           }).ToList());

           await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}