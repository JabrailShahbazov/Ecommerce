using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.File;

public class FileReadRepository : ReadRepository<EcommerceApi.Domain.Entities.File>, IFileReadRepository
{
    public FileReadRepository(EcommerceDbContext context) : base(context)
    {
    }
}