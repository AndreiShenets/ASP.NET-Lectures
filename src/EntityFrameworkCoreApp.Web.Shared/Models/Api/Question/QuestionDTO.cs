using System;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class QuestionDTO
    {
        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
