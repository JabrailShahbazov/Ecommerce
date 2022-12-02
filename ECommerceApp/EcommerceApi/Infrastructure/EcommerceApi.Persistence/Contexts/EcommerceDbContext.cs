using EcommerceApi.Domain.Entities;
using EcommerceApi.Domain.Entities.Common.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Contexts;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //Interceptor
        var datas = ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
            switch (data.State)
            {
                case EntityState.Added:
                    data.Entity.CreateDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    data.Entity.UpdatedDate = DateTime.UtcNow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}