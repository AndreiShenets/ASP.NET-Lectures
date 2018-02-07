using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class Answer
    {
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public string Description { get; set; }
    }
}