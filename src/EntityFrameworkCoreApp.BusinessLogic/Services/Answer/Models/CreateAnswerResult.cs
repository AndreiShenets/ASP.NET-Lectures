using System;
using System.Collections.Generic;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class CreateAnswerResult : Result
    {
        public Guid AnswerId { get; set; }

        public CreateAnswerResult(Guid answerId) : base(succeeded: true)
        {
            this.AnswerId = answerId;
        }

        public CreateAnswerResult(params Error[] errors) : base(false, errors)
        {

        }

        public CreateAnswerResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
