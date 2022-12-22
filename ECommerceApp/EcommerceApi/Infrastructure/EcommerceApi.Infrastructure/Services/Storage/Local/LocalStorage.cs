using Ecommerce.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<(string fileName, string source)>> UploadAsync(string container, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, container);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> data = new();
        foreach (IFormFile file in files)
        {
            await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
            data.Add((file.Name, $"{container}\\{file.Name}"));
        }

        return data;
        //todo: Custom EX
    }

    public async Task DeleteAsync(string path, string fileName)
        => System.IO.File.Delete($"{path}\\{fileName}");


    public List<string> GetFiles(string path)
    {
        var directory = new DirectoryInfo(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
        => System.IO.File.Exists($"{path}\\{fileName}");

    #region Private

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {
            //todo log!
            throw ex;
        }
    }

    #endregion
}