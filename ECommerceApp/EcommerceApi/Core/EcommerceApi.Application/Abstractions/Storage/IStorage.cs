using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Abstractions.Storage;

public interface IStorage
{
    Task<List<(string fileName, string source)>> UploadAsync(string source, IFormFileCollection files);

    Task DeleteAsync(string source, string fileName);

    List<string> GetFiles(string source);

    bool HasFile(string source, string fileName);
}