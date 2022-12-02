using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities;

namespace Ecommerce.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(EcommerceDbContext context) : base(context)
    {
    }
}