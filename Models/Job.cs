using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DREAMCatcher.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string? JobTitle { get; set; }
        [Required]
        public string? JobDescription { get; set;}
        [Required]
        public string? JobRole { get; set; }
        [AllowNull]
        public string? JobExperience { get; set; }
        [Required]
        public string? JobSkill { get; set; }
        [Required]
        
        public string? Jobtype { get; set; }
        [AllowNull]
        public double Jobsalary { get; set; }
        [Required]
        public string? Date { get; set;}
        [Required]
        public int Total_req { get; set; }
        [Required]
        public string? Locations { get; set; }

        [DefaultValue(0)]
        public int Total_select { get; set; }
        [DefaultValue('Y')]
        public char status { get; set; }
    }
}
