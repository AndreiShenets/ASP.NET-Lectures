namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class Error
    {
        public string Code { get; private set; }

        public string Description { get; private set; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
