using EcommerceApi.Domain.Entities.Common.Abstracts;

namespace EcommerceApi.Domain.Entities.Common.Concrete;

public abstract class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual DateTime UpdatedDate { get; set; }
}