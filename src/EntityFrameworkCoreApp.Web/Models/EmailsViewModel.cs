using System.Collections.Generic;

namespace EntityFrameworkCoreApp.Web.Models
{
    public class EmailsViewModel
    {
        public IEnumerable<EmailViewModel> Emails { get; set; }
    }
}
