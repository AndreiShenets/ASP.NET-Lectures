using AspNetCoreQuartzApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCoreQuartzApp.Jobs
{
    public static class JobBuilderExtensions
    {
        public static IServiceCollection AddJobs(this IServiceCollection services)
        {
            return services
                .AddSingleton<IScheduler>(t => StdSchedulerFactory.GetDefaultScheduler().Result);
        }

        public static IApplicationBuilder UseJobs(this IApplicationBuilder app)
        {
            IScheduler scheduler = app.ApplicationServices.GetService<IScheduler>();
            IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            List<JobSettings> jobSettingsList = app.ApplicationServices.GetService<IOptions<List<JobSettings>>>().Value;

            IDictionary<string, object> jobParams = new Dictionary<string, object>
                {
                    { nameof(IServiceScopeFactory), serviceScopeFactory },
                    { nameof(JobTriggerType), JobTriggerType.Automatic }
                };

            using (var scope = serviceScopeFactory.CreateScope())
            {
                
                IDictionary<string, JobSettings> jobSettings = jobSettingsList.ToDictionary(t => t.Name, t => t);

                List<Type> jobTypes = Assembly
                    .GetCallingAssembly()
                    .GetTypes()
                    .Where(t => t.BaseType != null)
                    .Where(t => t.BaseType.IsGenericType)
                    .Where(t => t.BaseType.GetGenericTypeDefinition() == typeof(BaseJob<>))
                    .ToList();

                foreach (var jobType in jobTypes)
                {
                    IJobDetail jobDetail = JobBuilder.Create(jobType)
                        .WithIdentity(new JobKey(jobType.Name))
                        .UsingJobData(new JobDataMap(jobParams))
                        .StoreDurably()
                        .Build();

                    var jobInfo = jobSettings[jobType.Name];
                    if (jobInfo.IsActive)
                    {
                        var trigger = TriggerBuilder
                            .Create()
                            .WithIdentity(new TriggerKey(jobType.Name))
                            .WithCronSchedule(jobInfo.CronSchedule)
                            .Build();

                        scheduler.ScheduleJob(jobDetail, trigger);
                    }
                    else
                    {
                        scheduler.AddJob(jobDetail, true);
                    }
                }
            }

            scheduler.Start();
            return app;
        }
    }
}
