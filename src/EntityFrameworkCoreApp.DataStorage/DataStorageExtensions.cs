using EntityFrameworkCoreApp.DataStorage.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace EntityFrameworkCoreApp.DataStorage
{
    public static class DataStorageExtensions
    {
        public static IServiceCollection AddDataStorage(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            return services;
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QADbContext>(options =>
            {
                options.UseSqlServer(
                    configuration["ConnectionStrings:DefaultConnection"],
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(DataStorageExtensions).Assembly.GetName().Name));
            });
        }

        public static IWebHost Migrate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var environment = configuration["ASPNETCORE_ENVIRONMENT"];

                using (var dbContext = scope.ServiceProvider.GetRequiredService<QADbContext>())
                {
                    dbContext.Database.Migrate();
                }
            }
            return webhost;
        }
    }
}
