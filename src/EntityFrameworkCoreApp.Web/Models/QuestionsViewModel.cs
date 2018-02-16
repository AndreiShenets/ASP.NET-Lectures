using System.Collections.Generic;

namespace EntityFrameworkCoreApp.Web.Models
{
    public class QuestionsViewModel
    {
        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
