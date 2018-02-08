using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public class AnswerRepository : BaseRepository<AnswerEntity>, IAnswerRepository
    {
        public AnswerRepository(
            QADbContext dbContext) : base(
                dbContext: dbContext)
        {

        }
    }
}
