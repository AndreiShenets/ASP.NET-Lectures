using System;

namespace EntityFrameworkCoreApp.DataStorage.Models
{
    public class AnswerFilter
    {
        public int? From { get; set; }

        public int? ToTake { get; set; }

        public Guid? QuestionId { get; set; }
    }
}
