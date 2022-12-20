using Ecommerce.Application.Services;
using Ecommerce.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}