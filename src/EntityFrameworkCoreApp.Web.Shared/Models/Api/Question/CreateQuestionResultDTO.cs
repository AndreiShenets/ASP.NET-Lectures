using System;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class CreateQuestionResultDTO : ResultDTO
    {
        public Guid QuestionId { get; set; }
    }
}
