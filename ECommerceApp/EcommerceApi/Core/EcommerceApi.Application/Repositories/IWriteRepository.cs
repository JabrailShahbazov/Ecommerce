using EcommerceApi.Domain.Entities.Common.Concrete;

namespace Ecommerce.Application.Repositories;

public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> AddAsync(TEntity model);

    Task<bool> AddManyAsync(List<TEntity> model);

    bool Remove(TEntity model);

    Task<bool> RemoveAsync(string id);

    bool RemoveMany(List<TEntity> model);

    Task<bool> UpdateAsync(TEntity model);

    bool UpdateMany(List<TEntity> model);

    Task<bool> UpdateAsync(TEntity model, string id);

    Task<int> SaveAsync();
}