using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCoreApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger Logger;
        protected IMapper Mapper;

        public BaseController(IMapper mapper, ILogger logger)
        {
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
