using AutoMapper;
using EntityFrameworkCoreApp.DataStorage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    internal class EmailGeneratorService : BaseService, IEmailGeneratorService, IDisposable
    {
        public EmailGeneratorService(
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ILogger<EmailGeneratorService> logger) : base(
                contextAccessor,
                mapper,
                logger)
        {

        }

        public EmailEntity GenerateVerifyQuestionEmail(QuestionEntity questionEntity)
        {
            string emailTitle = "Please verify question";
            string body = $"Token to verify: {questionEntity.Token}, Question Id: {questionEntity.QuestionId}";
            
            return new EmailEntity()
            {
                GenerateDateTimeUTC = DateTime.UtcNow,
                IsSent = false,
                Title = emailTitle,
                Body = body,
                SendTo = questionEntity.EmailAddress
            };
        }
    }
}
