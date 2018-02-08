using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public class EmailRepository : BaseRepository<EmailEntity>, IEmailRepository
    {
        public EmailRepository(
            QADbContext dbContext) : base(
                dbContext: dbContext)
        {

        }

        public async Task<IEnumerable<EmailEntity>> GetEmailsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await Entities.ToListAsync(cancellationToken);
            return result;
        }
    }
}
