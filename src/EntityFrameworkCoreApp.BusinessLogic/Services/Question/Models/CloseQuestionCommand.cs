using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class CloseQuestionCommand
    {
        public Guid QuestionId { get; set; }

        public string Token { get; set; }
    }
}
