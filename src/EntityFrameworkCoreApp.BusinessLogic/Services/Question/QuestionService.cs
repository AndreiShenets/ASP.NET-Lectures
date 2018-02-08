using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class QuestionService : BaseService, IQuestionService, IDisposable
    {
        public QuestionService(
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger<QuestionService> logger) : base(
                contextAccessor,
                mapper,
                logger)
        {

        }
    }
}
