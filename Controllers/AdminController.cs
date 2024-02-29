using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DREAMCatcher.Models;
using Microsoft.Win32;
using DREAMCatcher.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace DREAMCatcher.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController(ApplicationContext db, SignInManager<UserData> signInManager, UserManager<UserData> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private SignInManager<UserData> signInManager = signInManager;
        private UserManager<UserData> userManager = userManager;
        private RoleManager<IdentityRole> roleManager = roleManager;
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationContext _db = db;
        public  IActionResult Index()
        {
            ViewBag.users = _db.Employees.Count();
            ViewBag.company = _db.Employers.Count();
            ViewBag.job = _db.Job.Count();
            ViewBag.selects = _db.JobApplied.Where(x=> x.Status=="Accepted").Count();

            return View();
        }
        public IActionResult Users()
        {
            var Users = _db.Employees.ToList();
            List<int> jobapp = new List<int>();

            foreach(var i in Users)
            {
                
                jobapp.Add(_db.JobApplied.Where(x=> x.EmployeeId==i.Id).Count());
            }
            ViewBag.apply=jobapp;
            ViewBag.users=Users;
            return View("User");
        }
        public IActionResult Companies()
        {
            var Companies = _db.Employers.ToList();
            List<int> job = new List<int>();
            List<int> apply = new List<int>();
            foreach(var i in Companies)
            {
           
                var jb = _db.Job.Where(a => a.CompanyId == i.Id);
                job.Add(jb.Count());
                foreach(var j in jb.ToList())
                {
                    apply.Add(_db.JobApplied.Where(a => a.CompanyId == i.Id && a.JobId==j.Id && a.Status=="Accepted").Count());

                }
            }
            ViewBag.jobs = job;
            ViewBag.apply = apply;
            ViewBag.companies=Companies;
            ViewBag.crud = true;
            return View("Employer");
        }
        [HttpGet]
        [Route("/Admin/Company/{id}")]
        public JsonResult CompanyGet(int id)
        {
            var company = _db.Employers.Where(a => a.Id == id).Join(_db.Users,a=> a.UserId,b=> b.Id,(a,b)=> new {a,b}).First();
            return Json(new { company });
        }

        [HttpPost]
        public async Task<IActionResult> Companies(EmployerVM emp)
        {
            //Console.WriteLine(emp.Id);
            //Console.WriteLine(emp.Id);
           //Console.WriteLine("exec");
           if(emp.Id != 0)
            {
                Employer em = _db.Employers.Where(x=> x.Id==emp.Id).First();
                em.Company_Name = emp.Company_Name;
                em.Address = emp.Address;
                em.DOJ = DateTime.Today.ToString();
                em.Locations = emp.Locations;
                em.Id = emp.Id;
                em.Country = emp.Country;
                em.Phone = emp.Phone;
                if (emp.Logo != null)
                {
                    int random = new Random().Next(1000, 9999);
                    string filename = "Uploads\\" + random.ToString() + emp.Logo.FileName;
                    string filePath2 = Path.Combine("wwwroot", filename);
                    if (System.IO.File.Exists(em.Logo))
                    {
                        System.IO.File.Delete(em.Logo);
                    }
                    using (Stream fileStream = new FileStream(filePath2, FileMode.Create, FileAccess.Write))
                    {
                        emp.Logo.CopyTo(fileStream);
                    }
                    em.Logo = filename;

                }
                _db.Employers.Update(em);
                _db.SaveChanges();
                return RedirectToAction("Companies");
            }

            UserData userData = new()
            {
                Email = emp.Email,
                UserName = emp.Email,
            };
            var result = await userManager.CreateAsync(userData, emp.Company_Name+"@123"!);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(userData, "Employer");
                string? id = _db.Users.Where(a=> a.Email==emp.Email).First().Id;
                Employer empl = new Employer();
                empl.Company_Name=emp.Company_Name;
                empl.Address = emp.Address;
                empl.DOJ= DateTime.Today.ToString();
                empl.Locations = emp.Locations;
                empl.UserId = id;
                empl.Country = emp.Country;
                empl.Phone = emp.Phone;
                if (emp.Logo != null)
                {
                    int random = new Random().Next(1000, 9999);
                    string filename = "Uploads\\" + random.ToString() + emp.Logo.FileName;
                    string filePath2 = Path.Combine("wwwroot", filename);

                    using (Stream fileStream = new FileStream(filePath2, FileMode.Create, FileAccess.Write))
                    {
                        emp.Logo.CopyTo(fileStream);
                    }
                    empl.Logo = filename;

                }
                _db.Employers.Add(empl);
                _db.SaveChanges();
            } 
            return RedirectToAction("Companies");
        }
        
        public IActionResult Jobs()
        {
            var Job = _db.Job.Join(_db.Employers,a=>a.CompanyId,b=>b.Id,(a,b)=> new {
              a.Date,b.Company_Name,a.Jobtype,a.Id,b.Logo
            }).ToList();
            List<List<int>> job_app = new List<List<int>>();
            foreach(var i in Job)
            {
                job_app.Add([_db.JobApplied.Where(a=> a.JobId==i.Id).Count(), _db.JobApplied.Where(a => a.JobId == i.Id && a.Status=="Accepted").Count()]);
            }
            ViewBag.job = Job;
            ViewBag.job_app = job_app;
            return View("Job");
        }
        public IActionResult Account()
        {
            var admins = userManager.GetUsersInRoleAsync("Admin").Result.ToList();
            ViewBag.users= admins;
            
            ViewBag.crud = true;
            return View("Admins");
        }
        [HttpGet]
        [Route("/Company/Delete/{id}")]
        public async Task<JsonResult> DelCompany(string id)
        {
            var employee = _db.Employers.Where(a => a.UserId == id).ExecuteDelete();
            _db.SaveChanges();
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);

            return Json(new { id });
        }
        [HttpGet]
        [Route("/Admin/Delete/{id}")]
        public async Task<JsonResult> DelAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            
            return Json(new { id });
        }
        [HttpPost]
        public async Task<IActionResult> Account(AdminVM admin)
        {
            Console.WriteLine(admin.Email);
            UserData userData = new()
            {
                Email = admin.Email,
                UserName = admin.Email
            };
            var result = await userManager.CreateAsync(userData, admin.Password!);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(userData, "Admin");
            }
                return RedirectToAction("Account");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Home");
        }
    }
}
