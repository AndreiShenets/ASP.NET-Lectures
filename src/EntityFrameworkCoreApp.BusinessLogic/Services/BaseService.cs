using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public abstract class BaseService : IDisposable
    {
        protected readonly HttpContext HttpContext;

        private bool disposed;

        protected CancellationToken CancellationToken => HttpContext?.RequestAborted ?? CancellationToken.None;
        
        protected internal ILogger Logger { get; set; }

        protected IMapper Mapper { get; set; }

        public BaseService(
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger logger)
        {
            this.HttpContext = contextAccessor?.HttpContext;
            this.Logger = logger;
            this.Mapper = mapper;
        }

        protected virtual void DisposeRepositories()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                DisposeRepositories();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
