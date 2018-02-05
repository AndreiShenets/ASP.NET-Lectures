using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSimpleAuthApp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AuthorizeOnly()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AllowAll()
        {
            return View();
        }
    }
}
