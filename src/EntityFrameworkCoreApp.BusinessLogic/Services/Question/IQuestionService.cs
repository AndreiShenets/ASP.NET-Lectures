using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();

        Task<Question> GetQuestionAsync(int questionId);
    }
}
