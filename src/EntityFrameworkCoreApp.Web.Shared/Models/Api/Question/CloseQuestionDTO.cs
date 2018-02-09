using System;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class CloseQuestionDTO
    {
        public Guid QuestionId { get; set; }

        public string Token { get; set; }
    }
}
