using AutoMapper;
using EntityFrameworkCoreApp.DataStorage.Models;
using EntityFrameworkCoreApp.DataStorage.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class AnswerService : BaseService, IAnswerService, IDisposable
    {
        protected IAnswerRepository AnswerRepository { get; set; }

        protected IQuestionRepository QuestionRepository { get; set; }

        public AnswerService(
            IAnswerRepository answerRepository,
            IQuestionRepository questionRepository,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger<AnswerService> logger) : base(
                contextAccessor,
                mapper,
                logger)
        {
            AnswerRepository = answerRepository ?? throw new ArgumentNullException(nameof(answerRepository));
            QuestionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }

        public async Task<Answer> GetAnswerAsync(Guid answerId)
        {
            ThrowIfDisposed();
            AnswerEntity answer = await AnswerRepository.GetAnswerAsync(answerId, CancellationToken);

            if (answer == null)
            {
                return null;
            }

            QuestionEntity question = await QuestionRepository.GetQuestionAsync(answer.QuestionId, CancellationToken);

            if (question == null || question.IsClosed || !question.IsVerified)
            {
                return null;
            }

            var answerResult = Mapper.Map<AnswerEntity, Answer>(answer);
            return answerResult;
        }

        public async Task<IEnumerable<Answer>> GetAnswersAsync(Guid questionId, int from, int toTake)
        {
            ThrowIfDisposed();
            QuestionEntity question = await QuestionRepository.GetQuestionAsync(questionId, CancellationToken);

            if (question == null || question.IsClosed || !question.IsVerified)
            {
                return Enumerable.Empty<Answer>();
            }

            AnswerFilter filter = new AnswerFilter()
            {
                QuestionId = questionId,
                From = from,
                ToTake = toTake
            };

            IEnumerable<AnswerEntity> answers = await AnswerRepository.GetAnswersAsync(filter, CancellationToken);
            
            var answersResult = Mapper.Map<IEnumerable<AnswerEntity>, IEnumerable<Answer>>(answers);
            return answersResult;
        }

        public async Task<CreateAnswerResult> CreateAnswerAsync(CreateAnswerCommand command)
        {
            ThrowIfDisposed();
            QuestionEntity question = await QuestionRepository.GetQuestionAsync(command.QuestionId, CancellationToken);

            if (question == null || question.IsClosed || !question.IsVerified)
            {
                return new CreateAnswerResult(new Error("Can't create answer", "Question is not valid."));
            }

            AnswerEntity answerEntity = new AnswerEntity()
            {
                Description = command.Description,
                CreateDateTimeUTC = DateTime.UtcNow,
                QuestionId = question.QuestionId
            };

            await AnswerRepository.CreateAsync(answerEntity, CancellationToken);
            return new CreateAnswerResult(answerEntity.QuestionId);
        }
    }
}
