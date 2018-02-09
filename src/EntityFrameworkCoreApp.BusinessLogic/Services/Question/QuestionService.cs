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
    public class QuestionService : BaseService, IQuestionService, IDisposable
    {
        protected IQuestionRepository QuestionRepository { get; set; }

        public QuestionService(
            IQuestionRepository questionRepository,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger<QuestionService> logger) : base(
                contextAccessor,
                mapper,
                logger)
        {
            QuestionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }

        public async Task<CloseQuestionResult> CloseQuestionAsync(CloseQuestionCommand command)
        {
            ThrowIfDisposed();
            QuestionEntity questionEntity = await QuestionRepository.GetQuestionAsync(command.QuestionId);

            if (questionEntity == null)
            {
                return new CloseQuestionResult(new Error("Not Found","Question not found"));
            }

            if (questionEntity.IsClosed)
            {
                return new CloseQuestionResult(new Error("Can't close", "Question is already closed"));
            }

            if (questionEntity.Token != command.Token)
            {
                return new CloseQuestionResult(new Error("Can't close", "Token is not valid"));
            }

            questionEntity.IsClosed = true;

            await QuestionRepository.UpdateAsync(questionEntity, CancellationToken);
            return new CloseQuestionResult();
        }

        public async Task<CreateQuestionResult> CreateQuestionAsync(CreateQuestionCommand command)
        {
            ThrowIfDisposed();
            QuestionEntity questionEntity = new QuestionEntity()
            {
                Description = command.Description,
                Name = command.Name,
                Email = command.Email,
                IsClosed = false,
                IsVerified = false,
                CreateDateTimeUTC = DateTime.UtcNow,
                Token = GenerateToken()
            };
            
            await QuestionRepository.CreateAsync(questionEntity, CancellationToken);
            return new CreateQuestionResult(questionEntity.QuestionId);
        }

        public async Task<Question> GetQuestionAsync(Guid questionId)
        {
            ThrowIfDisposed();
            QuestionEntity question = await QuestionRepository.GetQuestionAsync(questionId, CancellationToken);

            if (question == null || !question.IsVerified || question.IsClosed)
            {
                return null;
            }

            var questionResult = Mapper.Map<QuestionEntity,Question>(question);
            return questionResult;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync(int from, int toTake)
        {
            ThrowIfDisposed();

            QuestionFilter filter = new QuestionFilter
            {
                From = from, 
                ToTake = toTake,
                IsClosed = false,
                IsVerified = true
            };

            IEnumerable<QuestionEntity> questions = await QuestionRepository.GetQuestionsAsync(filter, CancellationToken);
            
            var questionsResult = Mapper.Map<IEnumerable<QuestionEntity>, IEnumerable<Question>>(questions);
            return questionsResult;
        }

        public async Task<VerifyQuestionResult> VerifyQuestionAsync(VerifyQuestionCommand command)
        {
            ThrowIfDisposed();
            QuestionEntity questionEntity = await QuestionRepository.GetQuestionAsync(command.QuestionId);

            if (questionEntity == null)
            {
                return new VerifyQuestionResult(new Error("Not Found", "Question not found"));
            }

            if (questionEntity.IsClosed)
            {
                return new VerifyQuestionResult(new Error("Can't verify", "Question is already verified"));
            }

            if (questionEntity.Token != command.Token)
            {
                return new VerifyQuestionResult(new Error("Can't verify", "Token is not valid"));
            }

            questionEntity.IsVerified = true;

            await QuestionRepository.UpdateAsync(questionEntity, CancellationToken);
            return new VerifyQuestionResult();
        }

        protected string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
