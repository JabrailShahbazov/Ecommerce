using EcommerceApi.Domain.Entities.Common.Abstracts;

namespace EcommerceApi.Domain.Entities.Common.Concrete;

public class SoftDelete : ISoftDelete
{
    public bool IsDelete { get; set; }
}