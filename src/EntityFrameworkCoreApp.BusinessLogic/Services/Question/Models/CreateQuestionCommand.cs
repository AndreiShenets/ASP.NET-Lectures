namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class CreateQuestionCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }
    }
}
