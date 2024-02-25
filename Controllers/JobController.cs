using Microsoft.AspNetCore.Mvc;

namespace DREAMCatcher.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View("JobMain");
        }
        public IActionResult Activity()
        {
            return View();
        }
    }
}
