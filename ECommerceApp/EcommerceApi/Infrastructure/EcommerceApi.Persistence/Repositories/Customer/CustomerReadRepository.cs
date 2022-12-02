using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities;

namespace Ecommerce.Persistence.Repositories;

public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(EcommerceDbContext context) : base(context)
    {
    }
}