using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DREAMCatcher.Models;

namespace DREAMCatcher.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {  
            return View();
        }
        public IActionResult Users()
        {
            return View("User");
        }
        public IActionResult Companies()
        {
            ViewBag.crud = true;
            return View("Employer");
        }
        public IActionResult Jobs()
        {
            return View("Job");
        }
        public IActionResult Account()
        {
            ViewBag.crud = true;
            return View("Admins");
        }
    }
}
