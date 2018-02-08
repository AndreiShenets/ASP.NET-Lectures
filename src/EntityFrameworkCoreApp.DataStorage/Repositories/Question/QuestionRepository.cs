using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(
            QADbContext dbContext) : base(
                dbContext: dbContext)
        {

        }

    }
}
