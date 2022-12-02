using EcommerceApi.Domain.Entities.Common.Abstracts;

namespace EcommerceApi.Domain.Entities.Common.Concrete;

public class AuditEntity : IAuditEntity
{
    /// <summary>
    /// CreateId
    /// </summary>
    public int CreateId { get; set; }

    /// <summary>
    /// CreateTime
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// UpdateId
    /// </summary>
    public int UpdateId { get; set; }

    /// <summary>
    /// UpdateTime
    /// </summary>
    public DateTime UpdateDate { get; set; }
}