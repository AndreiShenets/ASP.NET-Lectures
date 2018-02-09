using System.Collections.Generic;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class ResultDTO
    {
        public bool Succeeded { get; set; }

        public IEnumerable<ErrorDTO> Errors { get; set; }
    }
}
