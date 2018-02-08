using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCoreApp.Web.Controllers.Api
{
    public class BaseApiController : Controller
    {
        protected ILogger Logger;
        protected IMapper Mapper;

        public BaseApiController(IMapper mapper, ILogger logger)
        {
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected ObjectResult Exception(Exception exception, string message, params object[] args)
        {
            Logger.LogError(exception, message, args);
            return StatusCode(500, new { message = message, description = "Unknown error occured." });
        }
    }
}
