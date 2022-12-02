namespace EcommerceApi.Domain.Entities.Common.Abstracts;

public interface IAuditEntity
{
    public int CreateId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public int UpdateId { get; set; }
    
    public DateTime UpdateDate { get; set; }
}