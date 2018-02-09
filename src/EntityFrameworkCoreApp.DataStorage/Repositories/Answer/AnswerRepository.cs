using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public class AnswerRepository : BaseRepository<AnswerEntity>, IAnswerRepository
    {
        public AnswerRepository(
            QADbContext dbContext) : base(
                dbContext: dbContext)
        {

        }

        public async Task<AnswerEntity> GetAnswerAsync(Guid answerId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var result = await Entities.FirstOrDefaultAsync(t => t.AnswerId == answerId, cancellationToken);
            return result;
        }

        public async Task<IEnumerable<AnswerEntity>> GetAnswersAsync(AnswerFilter filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var entitiesQuery = ApplyFilter(Entities, filter);
            var result = await entitiesQuery.ToListAsync(cancellationToken);
            return result;
        }

        protected IQueryable<AnswerEntity> ApplyFilter(IQueryable<AnswerEntity> entities, AnswerFilter filter)
        {
            if (filter.QuestionId.HasValue)
            {
                entities = entities.Where(t => t.QuestionId == filter.QuestionId.Value);
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
