using AspNetCoreQuartzApp.Jobs;
using AspNetCoreQuartzApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace AspNetCoreQuartzApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Currency API", Version = "v1" });
            });

            services.AddJobs();

            services.AddSingleton<IJobInformationService, JobInformationService>();
            services.AddSingleton<ICurrencyService, CurrencyService>();

            services.Configure<List<JobSettings>>(Configuration.GetSection(nameof(JobSettings)));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency API");
            });

            app.UseMvc();

            app.UseJobs();
        }
    }
}
