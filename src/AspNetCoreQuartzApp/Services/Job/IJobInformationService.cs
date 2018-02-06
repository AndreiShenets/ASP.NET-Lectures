using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Services
{
    public interface IJobInformationService
    {
        Task CreateAsync(JobInformation jobInformation);

        Task UpdateAsync(JobInformation jobInformation);

        Task<IEnumerable<JobInformation>> GetJobInformationsAsync(int from = 0, int toTake = int.MaxValue);

        Task<bool> TriggerJobAsync(string jobName);
    }
}
