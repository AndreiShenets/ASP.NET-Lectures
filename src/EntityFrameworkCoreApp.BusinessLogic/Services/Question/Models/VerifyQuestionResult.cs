using System.Collections.Generic;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class VerifyQuestionResult : Result
    {
        public VerifyQuestionResult() : base(succeeded: true)
        {

        }

        public VerifyQuestionResult(params Error[] errors) : base(false, errors)
        {

        }

        public VerifyQuestionResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
