using EntityFrameworkCoreApp.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public interface IAnswerRepository : IRepository<AnswerEntity>
    {
        Task<AnswerEntity> GetAnswerAsync(Guid answerId, CancellationToken cancellationToken = default(CancellationToken));
        
        Task<IEnumerable<AnswerEntity>> GetAnswersAsync(AnswerFilter filter, CancellationToken cancellationToken = default(CancellationToken));
    }
}
