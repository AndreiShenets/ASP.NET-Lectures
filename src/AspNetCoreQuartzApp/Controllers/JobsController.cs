using AspNetCoreQuartzApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Controllers
{
    [Route("api")]
    public class JobsController : Controller
    {
        protected IJobInformationService jobInformationService;

        public JobsController(IJobInformationService jobInformationService)
        {
            this.jobInformationService = jobInformationService ?? throw new ArgumentNullException(nameof(jobInformationService));
        }

        [HttpPost("jobs")]
        public async Task<IActionResult> TriggerJobAsync([FromQuery]string jobName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(jobName))
                {
                    return BadRequest();
                }

                bool result = await jobInformationService.TriggerJobAsync(jobName);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetJobAsync()
        {
            try
            {
                var jobs = await jobInformationService.GetJobInformationsAsync();

                return Ok(jobs);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
