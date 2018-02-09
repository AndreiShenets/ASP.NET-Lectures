using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class VerifyQuestionCommand
    {
        public Guid QuestionId { get; set; }

        public string Token { get; set; }
    }
}
