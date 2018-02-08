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
    public class EmailController : BaseApiController
    {
        protected IEmailService EmailService { get; set; }

        public EmailController(
            IEmailService emailService,
            IMapper mapper, 
            ILogger<EmailController> logger) : base(
                mapper: mapper, 
                logger: logger)
        {
            EmailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailsAsync()
        {
            try
            {
                var emails = await EmailService.GetEmailsAsync();
                var emailResult = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(emails);

                return Ok(emailResult);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Failed to get emails");
            }
        }
    }
}
