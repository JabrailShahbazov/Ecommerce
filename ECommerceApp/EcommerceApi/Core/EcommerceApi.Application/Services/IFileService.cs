using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Services;

public interface IFileService
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFileCollection);

    Task<bool> CopyFileAsync(string path, IFormFile formFile);
}