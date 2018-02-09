using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetQuestionsAsync(int from, int toTake);

        Task<Question> GetQuestionAsync(Guid questionId);

        Task<CreateQuestionResult> CreateQuestionAsync(CreateQuestionCommand command);

        Task<VerifyQuestionResult> VerifyQuestionAsync(VerifyQuestionCommand command);

        Task<CloseQuestionResult> CloseQuestionAsync(CloseQuestionCommand command);
    }
}
