using EcommerceApi.Domain.Enums;

namespace Ecommerce.Application.Abstractions.Storage;

public interface IStorageService : IStorage
{
    public StorageType StorageType { get; }
}