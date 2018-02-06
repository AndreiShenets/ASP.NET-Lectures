using System;

namespace AspNetCoreQuartzApp.Services
{
    public class JobInformation
    {
        public int JobInformationId { get; set; }

        public string JobName { get; set; }
        
        public JobTriggerType TriggerType { get; set; }

        public DateTime? StartDateTimeUTC { get; set; }

        public DateTime? EndDateTimeUTC { get; set; }

        public bool IsFinishedWithError { get; set; }

        public string ErrorDescription { get; set; }

        public string WarningDescription { get; set; }

        public string ResultDescription { get; set; }
    }
}
