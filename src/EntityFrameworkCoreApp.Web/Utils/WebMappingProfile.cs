using AutoMapper;
using EntityFrameworkCoreApp.BusinessLogic.Services;
using EntityFrameworkCoreApp.Web.Models.Api;
using System.Linq;
using System.Reflection;

namespace EntityFrameworkCoreApp.Web
{
    public partial class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            this.GetType()
                .GetTypeInfo()
                .DeclaredMethods
                .Where(t => t.Name.Contains("Initialize"))
                .Where(t => t.IsPrivate)
                .Where(t => !t.IsStatic)
                .ToList()
                .ForEach(t => t.Invoke(this, null));
        }

        private void InitializeEmail()
        {
            CreateMap<Email, EmailDTO>();
        }
    }
}
