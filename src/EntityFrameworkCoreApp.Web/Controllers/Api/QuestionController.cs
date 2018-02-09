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
        public async Task<IActionResult> GetQuestionsAsync([FromQuery] int? from = 0)
        {
            try
            {
                var questions = await QuestionService.GetQuestionsAsync(from.Value, 10);
                var questionsResult = Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questions);

                return Ok(questionsResult);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Failed to get questions");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestionAsync([FromBody] CreateQuestionDTO createQuestionDTO)
        {
            try
            {
                var command = new CreateQuestionCommand
                {
                    Description = createQuestionDTO.Description,
                    Email = createQuestionDTO.Email,
                    Name = createQuestionDTO.Name
                };

                var createQuestionResult = await QuestionService.CreateQuestionAsync(command);
                var result = Mapper.Map<CreateQuestionResult, CreateQuestionResultDTO> (createQuestionResult);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Failed to create question");
            }
        }
    }
}
