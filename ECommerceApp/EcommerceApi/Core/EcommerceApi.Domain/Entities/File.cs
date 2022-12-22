using System.ComponentModel.DataAnnotations.Schema;
using EcommerceApi.Domain.Entities.Common.Concrete;
using EcommerceApi.Domain.Enums;

namespace EcommerceApi.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }
    
    public string Path { get; set; }

    public StorageType StorageType { get; set; }
    [NotMapped] public override DateTime UpdatedDate { get; set; }
}