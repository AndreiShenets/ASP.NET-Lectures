using AutoMapper;
using EntityFrameworkCoreApp.BusinessLogic.Services;
using EntityFrameworkCoreApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Web.Controllers
{
    public class QuestionController : BaseController
    {
        protected IQuestionService QuestionService { get; set; }

        protected IAnswerService AnswerService { get; set; }

        public QuestionController(
            IQuestionService questionService,
            IAnswerService answerService,
            IMapper mapper,
            ILogger<QuestionController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {
            QuestionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
            AnswerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
        }

        public async Task<IActionResult> Questions()
        {
            var questions = await QuestionService.GetQuestionsAsync(0, int.MaxValue);
            var questionsResult = Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>(questions);
            
            return View(new QuestionsViewModel() { Questions = questionsResult });
        }

        public async Task<IActionResult> Question(Guid id)
        {
            var question = await QuestionService.GetQuestionAsync(id);
            var questionResult = Mapper.Map<Question, QuestionViewModel>(question);

            var answers = await AnswerService.GetAnswersAsync(id, 0, int.MaxValue);
            var answersResult = Mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerViewModel>>(answers);

            return View(new DetailedQuestionViewModel() { Question = questionResult, Answers = answersResult });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionViewModel model)
        {
            try
            {
                CreateQuestionCommand command = new CreateQuestionCommand()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Email = model.Email
                };
                var result = await QuestionService.CreateQuestionAsync(command);
                if (result.Succeeded)
                {
                    return RedirectToAction("Questions");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Error while creating question");
                return View();
            }
        }
    }
}
