using System;

namespace EntityFrameworkCoreApp.Web.Models
{
    public class QuestionViewModel
    {
        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AnswersCount { get; set; }
    }
}
