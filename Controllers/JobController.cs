using DREAMCatcher.Data;
using DREAMCatcher.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DREAMCatcher.Controllers
{
    public class JobController(ApplicationContext db, SignInManager<UserData> signInManager, UserManager<UserData> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private SignInManager<UserData> signInManager = signInManager;
        private UserManager<UserData> userManager = userManager;
        private RoleManager<IdentityRole> roleManager = roleManager;
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationContext _db = db;

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var image = _db.Employees.Where(a => a.UserId == currentUserId).First();
            var Jobs = _db.Job.Where(a => a.status == 'Y').Join(_db.Employers, a => a.CompanyId, b => b.Id, (a, b) => new { a.JobTitle,a.JobDescription, a.JobSkill, b.Logo, a.Jobtype, a.JobExperience,a.Jobsalary,a.Id,b.Company_Name,a.CompanyId }).ToList();
            var jobapplied = _db.JobApplied.Where(a => a.EmployeeId == image.Id && a.Status == "Applied").ToList();
            List<int> ans=new List<int>();
            foreach(var j in jobapplied)
            {
                ans.Add(j.CompanyId);
                Console.WriteLine(j.CompanyId);
            }
            List<int> exp=new List<int>();
            List<int> sal = new List<int>();
            List<int> type = new List<int>();
            type.Add(_db.Job.Where(x => x.Jobtype == "On Site").Count());
            type.Add(_db.Job.Where(x => x.Jobtype == "Work From Home").Count());
            type.Add(_db.Job.Where(x => x.Jobtype == "Internship").Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary>=0.0 && x.Jobsalary <= 4.0).Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary > 4.0 && x.Jobsalary <= 10.0).Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary > 10.0).Count());
            exp.Add(_db.Job.Where(x => x.JobExperience=="Fresher").Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Mid-Level").Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Experienced").Count());
            ViewBag.exp = exp;
            ViewBag.sal = sal;
            ViewBag.type = type;



            ViewBag.applied = ans;
            ViewBag.jobs= Jobs;
            ViewBag.profile = image.Image.Replace("\\", "/");
            return View("JobMain");
        }
        [Authorize(Roles ="Employer")]
        public IActionResult Activity()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int companyId = _db.Employers.Where(a => a.UserId == currentUserId).First().Id;

            var ActiveJob = _db.Job.Where(a => a.CompanyId == companyId && a.status=='Y' ).ToList();
            var InactiveJob = _db.Job.Where(a => a.CompanyId == companyId && a.status == 'N').ToList();
            var jobapplied = (from p in _db.JobApplied
                              join q in _db.Job 
                              on p.JobId equals q.Id
                              join r in _db.Employees
                              on p.EmployeeId equals r.Id
                              where p.Status=="Applied"
                              select new
                              {
                                  Title=q.JobTitle,
                                  Name=(r.First_Name+" "+r.Last_Name),
                                  resume=r.Resume,
                                  image=r.Image,
                                  id=r.Id,
                                  userid=r.UserId,
                                  applyid=p.Id,
                                  status=p.Status
                              }
                              ).ToList();
            ViewBag.apply = jobapplied;
            ViewBag.jobs = ActiveJob;
            ViewBag.history = InactiveJob;

            var image = _db.Employers.Where(a => a.UserId == currentUserId).First();
            ViewBag.profile = image.Logo.Replace("\\", "/");
            return View();
        }
        [HttpPost]
        public IActionResult Activity(Job jb)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int companyId = _db.Employers.Where(a => a.UserId == currentUserId).First().Id;

            Job job;
            if (jb.Id == 0)
            {
                job = new Job();
                job.Date = DateTime.Today.ToString();

            }
            else
            {
                job = _db.Job.Where(x => x.Id == jb.Id).First();
                job.Id = jb.Id;
            }
            job.JobDescription = jb.JobDescription;
            job.Locations = jb.Locations;
            job.JobSkill = jb.JobSkill;
            job.JobRole = jb.JobRole;
            job.Total_req = jb.Total_req;
            job.Jobtype = jb.Jobtype;
            job.JobTitle = jb.JobTitle;
            job.CompanyId = companyId;
            job.JobExperience = jb.JobExperience;
            job.Jobsalary = jb.Jobsalary;
            job.status = 'Y';
            if(job.Id==0)
                _db.Add(job);
            else
                _db.Update(job);
            _db.SaveChanges();
            return RedirectToAction("Activity");
        }
        [HttpGet]
        [Route("/Company/Job/{id}")]
        public JsonResult GetJob(int id)
        {
            
            var job = _db.Job.Where(a=> a.Id==id).First();
            return Json(new { job });

        }
        [HttpGet]
        [Route("/Job/Details/{id}")]
        public JsonResult GetDetails(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var image = _db.Employees.Where(a => a.UserId == currentUserId).First();

            Job job =_db.Job.Where(x=> x.Id==id).First();
            string? status = "";
            try
            {
                status = _db.JobApplied.Where(a => a.EmployeeId == image.Id && a.JobId == job.Id).First().Status;
            }
            catch(Exception e)
            {
                status = "Not";
            }

            return Json(new { job,status });
        }
        [HttpGet]
        [Route("/Company/Status/{id}")]
        public JsonResult StatusJob(int id)
        {

            Job job = _db.Job.Where(a => a.Id == id).First();
            if (job.status == 'Y')
            {
                job.status = 'N';
            }
            else
                job.status = 'Y';
            _db.Update(job);
            _db.SaveChanges();
            return Json(new { job });

        }
        [HttpGet]
        [Route("/Job/Accept/{id}")]
        public JsonResult AddJob(int id)
        {
            JobApplied jp=_db.JobApplied.Where(a=> a.Id==id).First();
            jp.Status = "Accepted";
            _db.JobApplied.Update(jp);
            Job jb=_db.Job.Where(a=> a.Id==jp.JobId).First();
            jb.Total_select = jb.Total_select + 1;
            if (jb.Total_select == jb.Total_req)
            {
                jb.status = 'N';
            }
            _db.Job.Update(jb);
            _db.SaveChanges();

            string done = "done";
            return Json(new { done });

        }
        [HttpGet]
        [Route("/Job/Reject/{id}")]
        public JsonResult RejectJob(int id)
        {

            JobApplied jp = _db.JobApplied.Where(a => a.Id == id).First();
            jp.Status = "Rejected";
            _db.JobApplied.Update(jp);
            _db.SaveChanges();
            string done = "done";

            return Json(new { done });

        }
        [HttpPost]
        [Route("/Job/Apply")]
        public IActionResult ApplyJob(int id,int companyid)
        {
            JobApplied ja = new JobApplied();
            ja.JobId= id;
            ja.CompanyId = companyid;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int employeeid= _db.Employees.Where(a=>a.UserId== currentUserId).First().Id;
            ja.EmployeeId = employeeid;
            ja.Status = "Applied";
            _db.JobApplied.Add( ja );
            _db.SaveChanges();
            return RedirectToAction("");
        }
        [HttpPost]
        [Route("/Job/Find")]
        public IActionResult FindJob(string location, string types,string salary)
        {
            double lim1=0,lim2= Double.MaxValue;
            if(location==null)
            {
                location = "";
            }
            if (salary != "Any")
            {
                if (salary == "10")
                {
                    lim1 = 10;
                    lim2 = Double.MaxValue;
                }
                else
                {
                    lim1 = Convert.ToDouble(salary.Split("-")[0]);
                    lim2 = Convert.ToDouble(salary.Split("-")[1]);


                }
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var image = _db.Employees.Where(a => a.UserId == currentUserId).First();
            var Jobs = _db.Job.Where(a => a.status == 'Y' && a.Locations.Contains(location) && a.Jobsalary>=lim1 && a.Jobsalary<=lim2 && (a.Jobtype==types || types=="Any" )).Join(_db.Employers, a => a.CompanyId, b => b.Id, (a, b) => new { a.JobTitle, a.JobDescription, a.JobSkill, b.Logo, a.Jobtype, a.JobExperience, a.Jobsalary, a.Id, b.Company_Name, a.CompanyId }).ToList();
            var jobapplied = _db.JobApplied.Where(a => a.EmployeeId == image.Id && a.Status == "Applied").ToList();
            List<int> ans = new List<int>();
            foreach (var j in jobapplied)
            {
                ans.Add(j.CompanyId);
                Console.WriteLine(j.CompanyId);
            }
            List<int> exp = new List<int>();
            List<int> sal = new List<int>();
            List<int> type = new List<int>();
            type.Add(_db.Job.Where(x => x.Jobtype == "On Site").Count());
            type.Add(_db.Job.Where(x => x.Jobtype == "Work From Home").Count());
            type.Add(_db.Job.Where(x => x.Jobtype == "Internship").Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary >= 0.0 && x.Jobsalary <= 4.0).Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary > 4.0 && x.Jobsalary <= 10.0).Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary > 10.0).Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Fresher").Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Mid-Level").Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Experienced").Count());
            ViewBag.exp = exp;
            ViewBag.sal = sal;
            ViewBag.type = type;



            ViewBag.applied = ans;
            ViewBag.jobs = Jobs;
            ViewBag.profile = image.Image.Replace("\\", "/");
            return View("JobMain");

        }
        [Route("/Job/Filters")]
        public IActionResult FiltersJob(string salary="",string types="",string experience="")
        {
            //string? types = context.Request.Query["types"];
            //string? experience = context.Request.Query["experience"];
            //string? salary = context.Request.Query["salary"];
            
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var image = _db.Employees.Where(a => a.UserId == currentUserId).First();
             var Jobs = _db.Job.Where(a => a.status == 'Y' && (types=="" ||
           a.Jobtype==types)
            &&
           (salary=="" || (salary=="10+" && a.Jobsalary>10) ||  (a.Jobsalary >= Convert.ToDouble(salary.Substring(0,1)) && a.Jobsalary <= Convert.ToDouble(salary.Substring(2)))
           ) 
           &&
           (experience=="" || 
           a.JobExperience==experience
           )
           ).Join(_db.Employers, a => a.CompanyId, b => b.Id, (a, b) => new { a.JobTitle, a.JobDescription, a.JobSkill, b.Logo, a.Jobtype, a.JobExperience, a.Jobsalary, a.Id, b.Company_Name, a.CompanyId }).ToList();  

            
            var jobapplied = _db.JobApplied.Where(a => a.EmployeeId == image.Id && a.Status == "Applied").ToList();
            List<int> ans = new List<int>();
            foreach (var j in jobapplied)
            {
                ans.Add(j.CompanyId);
             //   Console.WriteLine(j.CompanyId);
            }
            List<int> exp = new List<int>();
            List<int> sal = new List<int>();
            List<int> type = new List<int>();
            type.Add(_db.Job.Where(x => x.Jobtype == "On Site").Count());
            type.Add(_db.Job.Where(x => x.Jobtype == "Work From Home").Count());
            type.Add(_db.Job.Where(x => x.Jobtype == "Internship").Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary >= 0.0 && x.Jobsalary <= 4.0).Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary > 4.0 && x.Jobsalary <= 10.0).Count());
            sal.Add(_db.Job.Where(x => x.Jobsalary > 10.0).Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Fresher").Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Mid-Level").Count());
            exp.Add(_db.Job.Where(x => x.JobExperience == "Experienced").Count());
            ViewBag.exp = exp;
            ViewBag.sal = sal;
            ViewBag.type = type;
            ViewBag.selectexp = experience;
            ViewBag.selectsal = salary;
            ViewBag.selecttypes = types;



            ViewBag.applied = ans;
            ViewBag.jobs = Jobs;
            ViewBag.profile = image.Image.Replace("\\", "/");
            return View("JobMain");

        } 
    }
}
