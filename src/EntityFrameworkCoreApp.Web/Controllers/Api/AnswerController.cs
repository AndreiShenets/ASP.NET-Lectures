using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class AnswerController : BaseApiController
    {
        public AnswerController(
            IMapper mapper, 
            ILogger<AnswerController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {

        }
    }
}
