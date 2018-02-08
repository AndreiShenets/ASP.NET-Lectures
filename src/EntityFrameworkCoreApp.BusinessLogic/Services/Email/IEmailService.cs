using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetEmailsAsync();
    }
}
