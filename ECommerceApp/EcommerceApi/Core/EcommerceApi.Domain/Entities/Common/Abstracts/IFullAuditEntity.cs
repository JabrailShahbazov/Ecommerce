namespace EcommerceApi.Domain.Entities.Common.Abstracts;

public interface IFullAuditEntity : IAuditEntity
{
    /// <summary>
    /// CreateId
    /// </summary>
    public int CreateId { get; set; }

    /// <summary>
    /// CreateDate
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// UpdateId
    /// </summary>
    public int UpdateId { get; set; }

    /// <summary>
    /// UpdateDate
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// DeleteId
    /// </summary>
    public int DeleteId { get; set; }

    /// <summary>
    /// DeleteDate
    /// </summary>
    public DateTime DeleteDate { get; set; }
}