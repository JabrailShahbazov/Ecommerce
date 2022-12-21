using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.InvoiceFile;

public class InvoiceFileWriteRepository: WriteRepository<EcommerceApi.Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(EcommerceDbContext context) : base(context)
    {
    }
}