using AutoMapper;
using EntityFrameworkCoreApp.DataStorage.Models;
using EntityFrameworkCoreApp.DataStorage.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class EmailService : BaseService, IEmailService, IDisposable
    {
        protected IEmailRepository EmailRepository { get; set; }

        public EmailService(
            IEmailRepository emailRepository,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger<EmailService> logger) : base(
                contextAccessor,
                mapper,
                logger)
        {
            this.EmailRepository = emailRepository ?? throw new ArgumentNullException(nameof(emailRepository));
        }

        public async Task<IEnumerable<Email>> GetEmailsAsync()
        {
            ThrowIfDisposed();
            var emails = await EmailRepository.GetEmailsAsync(CancellationToken);
            var result = Mapper.Map<IEnumerable<EmailEntity>, IEnumerable<Email>>(emails);
            return result;
        }
    }
}
