using AutoMapper;
using EntityFrameworkCoreApp.BusinessLogic.Services;
using EntityFrameworkCoreApp.Web.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class QuestionController : BaseApiController
    {
        protected IQuestionService QuestionService { get; set; }

        public QuestionController(
            IQuestionService questionService,
            IMapper mapper, 
            ILogger<QuestionController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {
            this.QuestionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionsAsync()
        {
            try
            {
                var questions = await QuestionService.GetQuestionsAsync();
                var questionsResult = Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questions);

                return Ok(questionsResult);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Failed to get questions");
            }
        }
    }
}
