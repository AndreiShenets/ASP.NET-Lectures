using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DataStorage.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }

        DbContext Context { get; }

        bool AutoSaveChanges { get; set; }

        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
