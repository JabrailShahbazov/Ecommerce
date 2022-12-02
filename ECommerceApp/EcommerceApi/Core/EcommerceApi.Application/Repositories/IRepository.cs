using EcommerceApi.Domain.Entities.Common.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Table { get; }
}