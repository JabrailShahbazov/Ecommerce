using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.File;

public class FileWriteRepository: WriteRepository<EcommerceApi.Domain.Entities.File>,IFileWriteRepository
{
    public FileWriteRepository(EcommerceDbContext context) : base(context)
    {
    }
}