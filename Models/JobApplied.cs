using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DREAMCatcher.Models
{
    public class JobApplied
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        
        public int EmployeeId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public string? Status { get; set; }

    }
}
