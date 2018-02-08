using EntityFrameworkCoreApp.BusinessLogic.Services;
using EntityFrameworkCoreApp.DataStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCoreApp.BusinessLogic
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IAnswerService, AnswerService>()
                .AddScoped<IQuestionService, QuestionService>()
                .AddDataStorage(configuration);
        }
    }
}
