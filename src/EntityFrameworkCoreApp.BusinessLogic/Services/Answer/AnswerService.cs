using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class AnswerService : BaseService, IAnswerService, IDisposable
    {
        public AnswerService(
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
