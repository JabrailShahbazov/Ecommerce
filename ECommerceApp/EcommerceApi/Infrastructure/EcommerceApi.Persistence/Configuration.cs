using Microsoft.Extensions.Configuration;

namespace Ecommerce.Persistence;

static class Configuration
{
    public static string? ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/EcommerceApi.Api"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("PostgreSql");
        }
    }
}