using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities;

namespace Ecommerce.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(EcommerceDbContext context) : base(context)
    {
    }
}