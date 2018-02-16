using System;

namespace EntityFrameworkCoreApp.Web.Models
{
    public class EmailViewModel
    {
        public Guid EmailId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string SendTo { get; set; }

        public bool IsSent { get; set; }

        public DateTime GenerateDateTimeUTC { get; set; }

        public DateTime? SendDateTimeUTC { get; set; }
    }
}
