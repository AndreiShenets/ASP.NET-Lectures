using System;
using System.Collections.Generic;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class CreateQuestionResult : Result
    {
        public Guid QuestionId { get; set; }

        public CreateQuestionResult(Guid questionId) : base(succeeded: true)
        {
            this.QuestionId = questionId;
        }

        public CreateQuestionResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateQuestionResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
