using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.InvoiceFile;

public class InvoiceFileReadRepository:ReadRepository<EcommerceApi.Domain.Entities.InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(EcommerceDbContext context) : base(context)
    {
    }
}