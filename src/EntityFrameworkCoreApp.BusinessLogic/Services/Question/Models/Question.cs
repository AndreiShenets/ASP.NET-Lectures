using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class Question
    {
        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
