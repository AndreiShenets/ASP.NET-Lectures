namespace AspNetCoreQuartzApp.Jobs
{
    public class JobSettings
    {
        public string Name { get; set; }

        public string CronSchedule { get; set; }

        public bool IsActive { get; set; }
    }
}