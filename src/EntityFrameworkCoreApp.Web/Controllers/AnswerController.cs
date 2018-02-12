using AutoMapper;
using EntityFrameworkCoreApp.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Web.Controllers
{
    public class AnswerController : BaseController
    {
        protected IAnswerService AnswerService { get; set; }

        public AnswerController(
            IAnswerService answerService,
            IMapper mapper,
            ILogger<AnswerController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {
            AnswerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Guid questionId, string answer)
        {
            CreateAnswerCommand command = new CreateAnswerCommand()
            {
                Description = answer,
                QuestionId = questionId
            };

            await AnswerService.CreateAnswerAsync(command);
            return RedirectToAction("Question", "Question", new { id = questionId });
        }
    }
}
