using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.ProductImageFile;

public class ProductImageFileWriteRepository: WriteRepository<EcommerceApi.Domain.Entities.ProductImageFile>,IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(EcommerceDbContext context) : base(context)
    {
    }
}