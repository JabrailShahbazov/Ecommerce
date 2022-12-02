using Ecommerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;
using EcommerceApi.Domain.Entities.Common.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
{
    private readonly EcommerceDbContext _context;


    public WriteRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<bool> AddAsync(TEntity model)
    {
        var entities = await Table.AddAsync(model);
        return entities.State == EntityState.Added;
    }

    public async Task<bool> AddManyAsync(List<TEntity> model)
    {
        await Table.AddRangeAsync(model);
        return true;
    }

    public bool Remove(TEntity model)
    {
        var entity = Table.Remove(model);

        return entity.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var entityEntry = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        if (entityEntry == null) return false;
        var entity = Table.Remove(entityEntry);
        return entity.State == EntityState.Deleted;

    }

    public bool RemoveMany(List<TEntity> model)
    {
        Table.RemoveRange(model);

        return true;
    }

    public Task<bool> UpdateAsync(TEntity model)
    {
        var entityEntry = Table.Update(model);

        return Task.FromResult(entityEntry.State == EntityState.Modified);
    }

    public bool UpdateMany(List<TEntity> model)
    {
        Table.UpdateRange(model);

        return true;
    }

    public async Task<bool> UpdateAsync(TEntity model, string id)
    {
        var entities = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));

        if (entities == null) return false;
        entities = model;

        Table.Update(entities);
        return true;
    }

    public Task<int> SaveAsync()
        => _context.SaveChangesAsync();
}