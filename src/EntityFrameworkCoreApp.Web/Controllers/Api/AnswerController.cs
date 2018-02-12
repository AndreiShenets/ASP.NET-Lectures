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
    public class AnswerController : BaseApiController
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

        [HttpGet("{questionId:guid}")]
        public async Task<IActionResult> GetAnswersAsync(Guid questionId, [FromQuery] int? from = 0)
        {
            try
            {
                var answers = await AnswerService.GetAnswersAsync(questionId, from.Value, 10);
                var result = Mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerDTO>>(answers);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Failed to get answers");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswerAsync([FromBody] CreateAnswerDTO createAnswerDTO)
        {
            try
            {
                CreateAnswerCommand command = new CreateAnswerCommand()
                {
                    Description = createAnswerDTO.Description,
                    QuestionId = createAnswerDTO.QuestionId
                };

                var createAnswerResult  = await AnswerService.CreateAnswerAsync(command);
                var result = Mapper.Map<CreateAnswerResult, CreateAnswerResultDTO> (createAnswerResult);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Failed to create answer");
            }
        }
    }
}
