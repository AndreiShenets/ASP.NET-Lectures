using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EntityFrameworkCoreApp.DataStorage
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<QADbContext>
    {
        public QADbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Custom.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<QADbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new QADbContext(builder.Options);
        }
    }
}
