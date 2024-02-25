using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DREAMCatcher.Models;
namespace DREAMCatcher.Data
{
    public class ApplicationContext : IdentityDbContext<UserData>
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobApplied> JobApplied { get; set; }

    }
}
