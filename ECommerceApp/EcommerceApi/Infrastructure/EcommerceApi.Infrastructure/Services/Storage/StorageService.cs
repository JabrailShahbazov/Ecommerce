using Ecommerce.Application.Abstractions.Storage;
using EcommerceApi.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    private readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<List<(string fileName, string source)>> UploadAsync(string path, IFormFileCollection files)
        => await _storage.UploadAsync(path, files);

    public async Task DeleteAsync(string source, string fileName)
        => await _storage.DeleteAsync(source, fileName);

    public List<string> GetFiles(string source)
        => _storage.GetFiles(source);

    public bool HasFile(string source, string fileName)
        => _storage.HasFile(source, fileName);

    public StorageType StorageType
    {
        get
        {
            var name = _storage.GetType().Name;
            var storageType = name switch
            {
                "LocalStorage" => StorageType.Storage,
                "AzureStorage" => StorageType.Azure,
                "AWS" => StorageType.AWS,
                _ => StorageType.Storage
            };

            return storageType;
        }
    }
}