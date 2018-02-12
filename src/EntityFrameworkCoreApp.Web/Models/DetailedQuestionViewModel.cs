using System.Collections.Generic;

namespace EntityFrameworkCoreApp.Web.Models
{
    public class DetailedQuestionViewModel
    {
        public QuestionViewModel Question { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
