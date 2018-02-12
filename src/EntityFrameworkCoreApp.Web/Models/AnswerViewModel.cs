using System;

namespace EntityFrameworkCoreApp.Web.Models
{
    public class AnswerViewModel
    {
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public string Description { get; set; }
    }
}
