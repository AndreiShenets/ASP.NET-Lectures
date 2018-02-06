using Quartz;
using System;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Jobs
{
    [DisallowConcurrentExecution]
    public class NothingDoingJob : BaseJob<NothingDoingJob>
    {
        public NothingDoingJob() : base()
        {
        }
        
        protected override async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            await CreateJobInformationRecordAsync();
            try
            {
                await UpdateJobInformationRecordAsync("Job has finished correctly.");
                return;
            }
            catch (Exception exception)
            {
                await UpdateJobInformationRecordAsync("Job has finished with error.", exception);
                return;
            }
        }

    }
}
