using System;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class CreateAnswerDTO
    {
        public Guid QuestionId { get; set; }

        public string Description { get; set; }
    }
}
