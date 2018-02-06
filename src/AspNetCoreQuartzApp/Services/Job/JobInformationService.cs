using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Services
{
    public class JobInformationService : IJobInformationService
    {
        private int indexCounter = 0;

        protected ConcurrentDictionary<int, JobInformation> jobInformations = new ConcurrentDictionary<int, JobInformation>();

        protected IScheduler scheduler;

        public JobInformationService(IScheduler scheduler)
        {
            this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        protected int GetNextJobInformationId()
        {
            return Interlocked.Increment(ref indexCounter);
        }

        public Task CreateAsync(JobInformation jobInformation)
        {
            if (jobInformation == null)
            {
                throw new ArgumentNullException(nameof(jobInformation));
            }

            jobInformation.JobInformationId = GetNextJobInformationId();

            if (jobInformations.TryAdd(jobInformation.JobInformationId, jobInformation))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new Exception("Entity with same key already exist");
            }
        }

        public Task<IEnumerable<JobInformation>> GetJobInformationsAsync(int from = 0, int toTake = int.MaxValue)
        {
            var result = jobInformations
                .Select(t => t.Value)
                .OrderByDescending(t => t.JobInformationId)
                .Skip(from)
                .Take(toTake);

            return Task.FromResult(result);
        }

        public Task UpdateAsync(JobInformation jobInformation)
        {
            if (jobInformation == null)
            {
                throw new ArgumentNullException(nameof(jobInformation));
            }

            if (jobInformations.TryGetValue(jobInformation.JobInformationId, out JobInformation currentJobInformation))
            {
                if (ReferenceEquals(jobInformation, currentJobInformation))
                {
                    return Task.CompletedTask;
                }
            }
            else
            {
                throw new Exception("Entity to update do not exist");
            }
            
            if (jobInformations.TryUpdate(jobInformation.JobInformationId, jobInformation, null))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new Exception("Entity to update do not exist");
            }
        }

        public async Task<bool> TriggerJobAsync(string jobName)
        {
            var jobData = new JobDataMap
            {
                { nameof(JobTriggerType), JobTriggerType.Manual }
            };

            var existingJob = await scheduler.GetJobDetail(new JobKey(jobName));

            if (existingJob == null)
            {
                return false;
            }

            await scheduler.TriggerJob(new JobKey(jobName), jobData);
            return true;
        }
    }
}
