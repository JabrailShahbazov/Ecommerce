using System.Linq.Expressions;
using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities.Common.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
{
    private readonly EcommerceDbContext _context;

    public ReadRepository(EcommerceDbContext context)
    {
        _context = context;
    }


    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public IQueryable<TEntity> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking) query = query.AsNoTracking();

        return query;
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking) query = query.AsNoTracking();

        return query;
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity?> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
}