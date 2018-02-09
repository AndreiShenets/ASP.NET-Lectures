using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class CreateAnswerCommand
    {
        public Guid QuestionId { get; set; }

        public string Description { get; set; }
    }
}
