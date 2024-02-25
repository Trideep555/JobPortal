using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace DREAMCatcher.Models
{
    public class Education
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string? Institute_Name { get; set; }
        [Required]
        public string? Course_Name { get; set;  }
        [Required]
        public string? start_date {  get; set; }
        [Required]
        public string? end_date { get; set; }
        [Required]
        public double marks { get; set; }

    }
}
