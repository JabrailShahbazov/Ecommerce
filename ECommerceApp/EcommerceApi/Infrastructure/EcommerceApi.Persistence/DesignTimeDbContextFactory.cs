using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EcommerceDbContext>
{
    public EcommerceDbContext CreateDbContext(string[] args)
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new EcommerceDbContext(dbContextOptionsBuilder.Options);
    }
}