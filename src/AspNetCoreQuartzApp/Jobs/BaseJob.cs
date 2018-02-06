using AspNetCoreQuartzApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Jobs
{
    public abstract class BaseJob<TJob> : IJob
    {
        public string JobName { get; set; }

        protected ILogger<TJob> Logger { get; set; }

        protected IJobInformationService JobInformationService { get; set; }
        
        protected JobInformation JobInformation { get; set; }

        protected IJobExecutionContext JobExecutionContext { get; set; }

        public BaseJob()
        {
            this.JobName = typeof(TJob).Name;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            JobExecutionContext = context;
            IServiceScopeFactory scopeFactory = context.MergedJobDataMap["IServiceScopeFactory"] as IServiceScopeFactory;
            using (var scope = scopeFactory.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                Logger = serviceProvider.GetService<ILogger<TJob>>();
                JobInformationService = serviceProvider.GetService<IJobInformationService>();
                await ExecuteAsync(serviceProvider);
            }
        }

        protected abstract Task ExecuteAsync(IServiceProvider serviceProvider);

        protected async Task CreateJobInformationRecordAsync(JobInformation jobInformation = null)
        {
            try
            {
                if (jobInformation == null)
                {
                    var jobData = JobExecutionContext.MergedJobDataMap;
                    JobTriggerType triggerType = jobData.ContainsKey("JobTriggerType") ? (JobTriggerType)jobData["JobTriggerType"] : JobTriggerType.Automatic;

                    JobInformation = new JobInformation()
                    {
                        TriggerType = triggerType,
                        StartDateTimeUTC = DateTime.UtcNow,
                        JobName = JobName
                    };
                }
                else
                {
                    JobInformation = jobInformation;
                }
                await JobInformationService.CreateAsync(JobInformation);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Error while creating job information record.");
            }
        }

        protected async Task UpdateJobInformationRecordAsync(string description, string warning = null, JobInformation jobInformation = null, Exception exception = null)
        {
            try
            {
                if (jobInformation != null)
                {
                    JobInformation = jobInformation;
                }

                JobInformation.ResultDescription = description;
                JobInformation.EndDateTimeUTC = DateTime.UtcNow;
                JobInformation.WarningDescription = warning;

                if (exception == null)
                {
                    JobInformation.IsFinishedWithError = false;
                }
                else
                {
                    JobInformation.IsFinishedWithError = true;
                    JobInformation.ErrorDescription = exception.ToString();
                }

                await JobInformationService.UpdateAsync(JobInformation);
            }
            catch (Exception updateException)
            {
                Logger.LogError(updateException, "Error while updating job information record.");
            }
        }

        protected async Task UpdateJobInformationRecordAsync(string description, Exception exception)
        {
            await UpdateJobInformationRecordAsync(description, null, null, exception);
        }

        protected async Task UpdateJobInformationRecordAsync(string description)
        {
            await UpdateJobInformationRecordAsync(description, null, null, null);
        }

        protected async Task UpdateJobInformationRecordAsync(string description, string warning)
        {
            await UpdateJobInformationRecordAsync(description, warning, null, null);
        }

    }
}
