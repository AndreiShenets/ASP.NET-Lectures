using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSignalRApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
