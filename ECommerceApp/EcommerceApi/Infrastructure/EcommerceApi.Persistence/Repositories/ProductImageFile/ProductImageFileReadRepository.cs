using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.ProductImageFile;

public class ProductImageFileReadRepository : ReadRepository<EcommerceApi.Domain.Entities.ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(EcommerceDbContext context) : base(context)
    {
    }
}