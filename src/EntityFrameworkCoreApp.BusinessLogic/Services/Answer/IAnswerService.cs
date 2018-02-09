using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public interface IAnswerService
    {
        Task<Answer> GetAnswerAsync(Guid answerId);

        Task<IEnumerable<Answer>> GetAnswersAsync(Guid questionId, int from, int toTake);

        Task<CreateAnswerResult> CreateAnswerAsync(CreateAnswerCommand command);
    }
}
