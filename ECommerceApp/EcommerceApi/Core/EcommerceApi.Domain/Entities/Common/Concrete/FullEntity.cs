using EcommerceApi.Domain.Entities.Common.Abstracts;

namespace EcommerceApi.Domain.Entities.Common.Concrete;

public class FullEntity : BaseEntity, IFullAuditEntity
{
    public int CreateId { get; set; }
    public DateTime CreateDate { get; set; }
    public int UpdateId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int DeleteId { get; set; }
    public DateTime DeleteDate { get; set; }
}