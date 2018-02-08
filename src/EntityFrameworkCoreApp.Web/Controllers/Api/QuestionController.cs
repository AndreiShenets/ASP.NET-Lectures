using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class QuestionController : BaseApiController
    {
        public QuestionController(
            IMapper mapper, 
            ILogger<QuestionController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {

        }
    }
}
