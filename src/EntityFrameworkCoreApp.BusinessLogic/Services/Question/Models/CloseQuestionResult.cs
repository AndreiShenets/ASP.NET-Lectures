using System.Collections.Generic;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class CloseQuestionResult : Result
    {
        public CloseQuestionResult() : base(succeeded: true)
        {

        }

        public CloseQuestionResult(params Error[] errors) : base(false, errors)
        {

        }

        public CloseQuestionResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
