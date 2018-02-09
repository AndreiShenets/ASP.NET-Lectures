using System;

namespace EntityFrameworkCoreApp.Web.Models.Api
{
    public class VerifyQuestionDTO
    {
        public Guid QuestionId { get; set; }

        public string Token { get; set; }
    }
}
