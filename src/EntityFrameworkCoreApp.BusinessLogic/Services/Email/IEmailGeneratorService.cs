using EntityFrameworkCoreApp.DataStorage.Models;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    internal interface IEmailGeneratorService
    {
        EmailEntity GenerateVerifyQuestionEmail(QuestionEntity questionEntity);
    }
}
