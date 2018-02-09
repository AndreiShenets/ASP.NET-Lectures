using EntityFrameworkCoreApp.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public interface IQuestionRepository : IRepository<QuestionEntity>
    {
        Task<QuestionEntity> GetQuestionAsync(Guid questionId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<QuestionEntity>> GetQuestionsAsync(QuestionFilter filter, CancellationToken cancellationToken = default(CancellationToken));
    }
}
