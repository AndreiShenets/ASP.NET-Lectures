using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class Result
    {
        protected Result(bool succeeded, params Error[] errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors.ToList();
        }

        protected Result(bool succeeded, IEnumerable<Error> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors.ToList();
        }

        public bool Succeeded { get; protected set; }

        public IEnumerable<Error> Errors { get; protected set; }
    }
}
