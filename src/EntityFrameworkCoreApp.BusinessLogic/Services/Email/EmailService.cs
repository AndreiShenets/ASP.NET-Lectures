using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class EmailService : BaseService, IEmailService, IDisposable
    {
        public EmailService(
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger logger) : base(
                contextAccessor,
                mapper,
                logger)
        {

        }
    }
}
