using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;


namespace coreWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
