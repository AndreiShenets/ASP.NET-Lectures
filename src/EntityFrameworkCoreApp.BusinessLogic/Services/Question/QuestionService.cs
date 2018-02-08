using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<Question> GetQuestionAsync(int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
