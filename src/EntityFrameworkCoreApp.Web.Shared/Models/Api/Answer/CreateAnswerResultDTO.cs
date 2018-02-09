using System;

namespace EntityFrameworkCoreApp.Web.Models.Api.Answer
{
    public class CreateAnswerResultDTO : ResultDTO
    {
        public Guid AnswerId { get; set; }
    }
}
