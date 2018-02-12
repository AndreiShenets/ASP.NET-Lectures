using AutoMapper;
using EntityFrameworkCoreApp.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Web.Controllers
{
    public class EmailController : BaseController
    {
        protected IQuestionService QuestionService { get; set; }
        
        public EmailController(
            IQuestionService questionService,
            IMapper mapper,
            ILogger<EmailController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {
            QuestionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
        }

        public async Task<IActionResult> Verify([FromQuery] Guid questionId, [FromQuery] string token)
        {
            VerifyQuestionCommand command = new VerifyQuestionCommand()
            {
                QuestionId = questionId,
                Token = token
            };

            var result = await QuestionService.VerifyQuestionAsync(command);

            if (result.Succeeded)
            {
                ViewBag.Result = "Verified";
            }
            else
            {
                ViewBag.Result = "Failed";
            }

            return View("Result");
        }

        public async Task<IActionResult> Close([FromQuery] Guid questionId, [FromQuery] string token)
        {
            CloseQuestionCommand command = new CloseQuestionCommand()
            {
                QuestionId = questionId,
                Token = token
            };

            var result = await QuestionService.CloseQuestionAsync(command);

            if (result.Succeeded)
            {
                ViewBag.Result = "Closed";
            }
            else
            {
                ViewBag.Result = "Failed";
            }

            return View("Result");
        }
    }
}
