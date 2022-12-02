namespace EcommerceApi.Domain.Entities.Common.Abstracts;

public interface IFullEntity : IBaseEntity, ISoftDelete, IFullAuditEntity
{
    public int CreateId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public int UpdateId { get; set; }
    
    public DateTime UpdateDate { get; set; }
    
    public int DeleteId { get; set; }
    
    public DateTime DeleteDate { get; set; }
    
    public bool IsDelete { get; set; }
}