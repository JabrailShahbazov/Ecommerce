using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Infrastructure.Services.Storage;
using Ecommerce.Infrastructure.Services.Storage.Azure;
using Ecommerce.Infrastructure.Services.Storage.Local;
using EcommerceApi.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
    }

    // //Best Practice
    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }

    //It's not Practice
    public static void AddStorage(this IServiceCollection services, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Storage:
                services.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                services.AddScoped<IStorage, AzureStorage>();
                break;
            case StorageType.AWS:
                break;
            default:
                services.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}