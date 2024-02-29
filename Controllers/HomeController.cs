using DREAMCatcher.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Hosting.Internal;
using DREAMCatcher.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DREAMCatcher.Controllers
{
    public class HomeController(ApplicationContext db, SignInManager<UserData> signInManager, UserManager<UserData> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private SignInManager<UserData> signInManager = signInManager;
        private UserManager<UserData> userManager = userManager;
        private RoleManager<IdentityRole> roleManager = roleManager;
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationContext _db = db;

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (User.Identity.IsAuthenticated)
            {

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.IsInRole("User"))
                {
                    var image = _db.Employees.Where(a => a.UserId == currentUserId).First();
                    ViewBag.profile = image.Image.Replace("\\", "/");
                }
                else
                {
                    var image = _db.Employers.Where(a => a.UserId == currentUserId).First();
                    ViewBag.profile = image.Logo.Replace("\\", "/");
                }


            }
            var logos = _db.Employers.Select(a => a.Logo).OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            var Jobs = _db.Job.Where(a => a.status == 'Y').Join(_db.Employers, a => a.CompanyId, b => b.Id, (a, b) => new { a.JobTitle, a.Date, a.JobSkill, b.Logo, a.Jobtype, a.JobExperience }).Take(6).ToList();
            ViewBag.logos = logos;
            ViewBag.jobs = Jobs;

            return View("Index");
        }

        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.IsInRole("User"))
                {
                    var image = _db.Employees.Where(a => a.UserId == currentUserId).First();
                    ViewBag.profile = image.Image.Replace("\\", "/");
                }
                else
                {
                    var image = _db.Employers.Where(a => a.UserId == currentUserId).First();
                    ViewBag.profile = image.Logo.Replace("\\", "/");
                }
            }
            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.Username!, login.Password!, false, false);

                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                        return RedirectToAction("Index", "Admin");
                    return RedirectToAction("");
                }
            }
            ModelState.AddModelError("Password", "Invalid Login Credentials");
            return View(login);
        }
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                UserData userData = new()
                {
                    Email = register.Email,
                    UserName = register.Email
                };
                var result = await userManager.CreateAsync(userData, register.Password!);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userData, "User");
                    string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    Employee emp = new Employee();
                    Console.WriteLine(userData.Id);
                    emp.UserId = userData.Id;
                    emp.Address = register.Address;
                    emp.DOB = register.DOB;
                    emp.First_Name = register.First_Name;
                    emp.Last_Name = register.Last_Name;
                    emp.Country = register.Country;
                    emp.Phone = register.Phone;
                    emp.State = register.State;
                    emp.Skills = register.Skills;
                    string uploads = Path.Combine("Uploads");
                    Directory.CreateDirectory(uploads);
                    if (register.Image != null)
                    {
                        int random2 = new Random().Next(1000, 9999);

                        string filename2 = uploads + random2.ToString() + register.Image.FileName;
                        string filePath = Path.Combine("wwwroot", filename2);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            register.Image.CopyTo(fileStream);
                        }
                        emp.Image = filename2;
                    }
                    emp.Link = register.Link;
                    emp.Git = register.Git;
                    if (register.Resume == null)
                    {
                        return RedirectToAction("Index");
                    }
                    int random = new Random().Next(1000, 9999);
                    string filename = uploads + random.ToString() + register.Resume.FileName;
                    string filePath2 = Path.Combine("wwwroot", filename);

                    using (Stream fileStream = new FileStream(filePath2, FileMode.Create, FileAccess.Write))
                    {
                        register.Resume.CopyTo(fileStream);
                    }
                    emp.Resume = filename;
                    emp.DOJ = DateTime.Today.ToString();
                    _db.Employees.Add(emp);
                    _db.SaveChanges();
                    Education education = new Education();
                    Employee emp_id = _db.Employees.Where(a => a.UserId == userData.Id).First();
                    education.EmployeeId = emp_id.Id;
                    education.Institute_Name = register.s_schoolname;
                    education.Course_Name = register.s_streamname;
                    education.start_date = register.s_startdate;
                    education.end_date = register.s_enddate;
                    education.marks = register.s_marks;
                    _db.Educations.Add(education);
                    Education education2 = new Education();
                    education2.EmployeeId = emp_id.Id;
                    education2.Institute_Name = register.ss_schoolname;
                    education2.Course_Name = register.ss_streamname;
                    education2.start_date = register.ss_startdate;
                    education2.end_date = register.ss_enddate;
                    education2.marks = register.ss_marks;
                    _db.Educations.Add(education2);
                    Education education3 = new Education();
                    education3.EmployeeId = emp_id.Id;
                    education3.Institute_Name = register.c_schoolname;
                    education3.Course_Name = register.c_streamname;
                    education3.start_date = register.c_startdate;
                    education3.end_date = register.c_enddate;
                    education3.marks = register.c_marks;
                    _db.Educations.Add(education3);
                    _db.SaveChanges();

                    await signInManager.SignInAsync(userData, false);

                    return RedirectToAction("Index");


                }
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Register");
        }
        [HttpPost]
        [Route("/Home/PhotoUpload")]
        public JsonResult photoupload(ProfilePicVM file)
        {
            Console.WriteLine(file.Photo);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Employee emp = _db.Employees.Where(a => a.UserId == currentUserId).First();
            if (System.IO.File.Exists(emp.Image))
            {
                System.IO.File.Delete(emp.Image);
            }
            int random = new Random().Next(1000, 9999);
            string filename = "Uploads\\" + random.ToString() + file.Photo.FileName;
            string filePath2 = Path.Combine("wwwroot", filename);
            using (Stream fileStream = new FileStream(filePath2, FileMode.Create, FileAccess.Write))
            {
                file.Photo.CopyTo(fileStream);
            }
            emp.Image = filename;

            _db.Employees.Update(emp);
            _db.SaveChanges();

            return Json(new { file });
        }
        [Route("/Home/Profile/{id}")]
        public IActionResult ProfileView(string id)
        {
            var userData = _db.Employees.Where(a => a.UserId == id).First();
            if (userData.Image != null)
                userData.Image = userData.Image.Replace("\\", "/");
            ViewBag.profile = userData.Image;

            ViewBag.details = userData;
            var education = _db.Educations.Where(a => a.EmployeeId == userData.Id).ToList();
            ViewBag.education = education;
            var experience = _db.Experiences.Where(a => a.EmployeeId == userData.Id).ToList();
            ViewBag.experience = experience;
            ViewBag.view = true;
            return View("Profile");

        }


        [Authorize]
        public IActionResult Profile()
        {

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Employer"))
            {
                var employerData=_db.Employers.Where(a=> a.UserId == currentUserId).First();
                var email = _db.Users.Where(b => b.Id == currentUserId).Select(a => a.Email).First();
               
                ViewBag.details = employerData;
                ViewBag.Email=email;
                var image = _db.Employers.Where(a => a.UserId == currentUserId).First();
                ViewBag.profile = image.Logo.Replace("\\", "/");
                return View("CompanyProfile");
            }
                var userData = _db.Employees.Where(a => a.UserId == currentUserId).First();
                if(userData.Image!=null)
                userData.Image = userData.Image.Replace("\\", "/");
            var apply = (from p in _db.JobApplied
                         join w in _db.Employers
                         on p.CompanyId equals w.Id
                         join x in _db.Job
                         on p.JobId equals x.Id

                         where (p.EmployeeId == userData.Id)

                         select new
                         {
                             Company = w.Company_Name,
                             Title = x.JobTitle,
                             status = p.Status,
                         }).ToList();
            ViewBag.application = apply;
                ViewBag.profile = userData.Image;

            ViewBag.details = userData;
                var education = _db.Educations.Where(a => a.EmployeeId == userData.Id).ToList();
                ViewBag.education = education;
                var experience = _db.Experiences.Where(a => a.EmployeeId == userData.Id).ToList();
                ViewBag.experience = experience;
                return View();
        }
        [HttpPost]
        public IActionResult CompProfile(EmployerVM emp)
        {
            Employer em = _db.Employers.Where(x => x.Id == emp.Id).First();
            em.Company_Name = emp.Company_Name;
            em.Address = emp.Address;
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
            return RedirectToAction("Profile");

        }
        [HttpPost]
        public  async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpPost]
        [Route("/Home/EditAbout")]
        public JsonResult editprofile(ProfileAboutVM profile)
        {
            Employee emp = _db.Employees.Where(a => a.Id == profile.Id).First();
            emp.DOB = profile.DOB;
            emp.First_Name = profile.First_Name;
            emp.Last_Name = profile.Last_Name;
            emp.State=profile.State;
            emp.Address = profile.Address;
            emp.Phone=profile.Phone;
            emp.Link=profile.Link;
            emp.Git=profile.Git;
            emp.State = profile.State;
            emp.Country=profile.Country;
            emp.Id = profile.Id;
            if(profile.Resume != null)
            {
                if (System.IO.File.Exists(emp.Resume))
                {
                    System.IO.File.Delete(emp.Resume);
                }
                int random = new Random().Next(1000, 9999);
                string filename= "Uploads\\"+random.ToString() + profile.Resume.FileName;
                string filePath2 = Path.Combine("wwwroot",filename );
                using (Stream fileStream = new FileStream(filePath2, FileMode.Create, FileAccess.Write))
                {
                    profile.Resume.CopyTo(fileStream);
                }
                emp.Resume = filename;

            }
            _db.Employees.Update(emp);
            _db.SaveChanges();


            return Json(new { profile });
             
        }
        [HttpGet]
        [Route("/Home/Edu/{id}")]
        public JsonResult eduget(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var edu = _db.Educations.Join(_db.Employees, a => a.EmployeeId, b => b.Id, (a, b) => new { A=a,B=b }).Where(X=> X.B.UserId==currentUserId).ToList();
            return Json(new { edu });
        }
        [HttpGet]
        [Route("/Home/Exp/{id}")]
        public JsonResult expget(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var edu = _db.Experiences.Join(_db.Employees, a => a.EmployeeId, b => b.Id, (a, b) => new { A = a, B = b }).Where(X => X.B.UserId == currentUserId).ToList();
            return Json(new { edu });
        }
        [HttpPost]
        [Route("/Home/EditEducation")]
        public JsonResult editeducation(Education edu)
        {
            Education ed = new Education();

            if (edu.Id != 0)
            {
                ed = _db.Educations.Where(a => a.Id == edu.Id).First();

            }
            ed.Institute_Name = edu.Institute_Name;
            ed.Course_Name = edu.Course_Name;
            ed.start_date = edu.start_date;
            ed.end_date = edu.end_date;
            ed.EmployeeId = edu.EmployeeId;
            ed.marks = edu.marks;
            if (edu.Id != 0)
            {
                ed.Id = edu.Id;
                _db.Educations.Update(ed);

            }
            else
                _db.Educations.Add(ed);
            _db.SaveChanges();

            return Json(new { edu });

        }
        [HttpPost]
        [Route("/Home/EditExperience")]
        public JsonResult editeducation(Experience edu)
        {
            Experience ed = new Experience();

            if (edu.Id != 0)
            {
                ed = _db.Experiences.Where(a => a.Id == edu.Id).First();

            }
            ed.Company = edu.Company;
            ed.Role = edu.Role;
            ed.start_date = edu.start_date;
            ed.end_date = edu.end_date;
            ed.EmployeeId = edu.EmployeeId;
            if (edu.Id != 0)
            {
                ed.Id = edu.Id;
                _db.Experiences.Update(ed);

            }
            else
                _db.Experiences.Add(ed);
            _db.SaveChanges();

            return Json(new { edu });

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
