using System;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class AnswerDTO
    {
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public string Description { get; set; }
    }
}
