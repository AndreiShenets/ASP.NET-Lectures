using Quartz;
using System;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Jobs
{
    [DisallowConcurrentExecution]
    public class ThrowingErrorJob : BaseJob<ThrowingErrorJob>
    {
        public ThrowingErrorJob() : base()
        {
        }
        
        protected override async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            await CreateJobInformationRecordAsync();
            try
            {
                throw new DivideByZeroException();
            }
            catch (Exception exception)
            {
                await UpdateJobInformationRecordAsync("Job has finished with error.", exception);
                return;
            }
        }

    }
}
