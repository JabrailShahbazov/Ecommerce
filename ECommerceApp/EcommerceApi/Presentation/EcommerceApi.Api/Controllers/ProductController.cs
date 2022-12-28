using System.Net;
using Ecommerce.Application.Abstractions.Storage;
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

        private readonly IStorageService _storageService;

        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;

        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        public ProductController(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IFileWriteRepository fileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IFileReadRepository fileReadRepository,
            IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;

            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _storageService = storageService;
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
        public async Task<IActionResult> Upload([FromQuery] string id)
        {
            var datas = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            var products = await _productReadRepository.GetByIdAsync(id);


            await _productImageFileWriteRepository.AddManyAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.source,
                StorageType = _storageService.StorageType,
                Product = new List<Product>() { products }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));

            return Ok(product?.ProductImageFiles
                .Select(p => new
                {
                    p.Path,
                    p.FileName
                }));
        }
    }
}