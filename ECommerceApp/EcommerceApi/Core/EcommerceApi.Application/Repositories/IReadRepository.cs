using System.Linq.Expressions;
using EcommerceApi.Domain.Entities.Common.Concrete;

namespace Ecommerce.Application.Repositories;

public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Get All Entities
    /// </summary>
    /// <returns>TEntity</returns>
    IQueryable<TEntity> GetAll(bool tracking = true);

    /// <summary>
    /// Get Entity with from operation
    /// </summary>
    /// <param name="method"></param>
    /// <returns>TEntity</returns>
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method,bool tracking = true);

    /// <summary>
    ///  Get Single entity
    /// </summary>
    /// <param name="method"></param>
    /// <returns>TEntity</returns>
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> method,bool tracking = true);

    /// <summary>
    /// Get by id
    /// </summary>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(string id,bool tracking = true);
    
    /// <summary>
    /// Get All Entities
    /// </summary>
    /// <returns>TEntity</returns>
    Task<int> GetCount(bool tracking = true);
}