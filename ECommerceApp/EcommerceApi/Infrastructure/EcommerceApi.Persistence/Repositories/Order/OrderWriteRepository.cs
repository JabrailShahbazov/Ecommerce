using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities;

namespace Ecommerce.Persistence.Repositories;

public class OrderWriteRepository :WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(EcommerceDbContext context) : base(context)
    {
    }
}