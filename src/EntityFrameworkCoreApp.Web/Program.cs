using EntityFrameworkCoreApp.DataStorage;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EntityFrameworkCoreApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Migrate().Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
