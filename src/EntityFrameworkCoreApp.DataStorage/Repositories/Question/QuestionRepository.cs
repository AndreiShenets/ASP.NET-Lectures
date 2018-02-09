using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(
            QADbContext dbContext) : base(
                dbContext: dbContext)
        {

        }

        public async Task<QuestionEntity> GetQuestionAsync(Guid questionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var result = await Entities.FirstOrDefaultAsync(t => t.QuestionId == questionId, cancellationToken);
            return result;
        }

        public async Task<IEnumerable<QuestionEntity>> GetQuestionsAsync(QuestionFilter filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var entitiesQuery = ApplyFilter(Entities, filter);
            var result = await entitiesQuery.ToListAsync(cancellationToken);
            return result;
        }

        protected IQueryable<QuestionEntity> ApplyFilter(IQueryable<QuestionEntity> entities, QuestionFilter filter)
        {
            if (filter.IsClosed.HasValue)
            {
                entities = entities.Where(t => t.IsClosed == filter.IsClosed.Value);
            }

            if (filter.IsVerified.HasValue)
            {
                entities = entities.Where(t => t.IsVerified == filter.IsVerified.Value);
            }

            if (filter.From.HasValue || filter.ToTake.HasValue)
            {
                entities = entities.OrderBy(t => t.CreateDateTimeUTC);

                if (filter.From.HasValue)
                {
                    entities = entities.Skip(filter.From.Value);
                }

                if (filter.ToTake.HasValue)
                {
                    entities = entities.Take(filter.ToTake.Value);
                }
            }

            return entities;
        }
    }
}
