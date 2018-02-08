using EntityFrameworkCoreApp.DataStorage.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public interface IEmailRepository : IRepository<EmailEntity>
    {
        Task<IEnumerable<EmailEntity>> GetEmailsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
