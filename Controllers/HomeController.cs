using DREAMCatcher.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DREAMCatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM login)
        {
            Console.WriteLine(login.Username + login.Password);
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Register(RegisterVM register)
		{
            Console.Write(register.Resume.FileName);
			return RedirectToAction("Index");
		}
		public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
