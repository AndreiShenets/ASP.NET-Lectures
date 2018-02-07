using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCoreApp.DataStorage
{
    public static class DataStorageExtensions
    {
        public static IServiceCollection AddDataStorage(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
