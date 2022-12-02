using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities;

namespace Ecommerce.Persistence.Repositories;

public class ProductReadRepository :ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(EcommerceDbContext context) : base(context)
    {
    }
}