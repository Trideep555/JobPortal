using System.ComponentModel.DataAnnotations;

namespace DREAMCatcher.Models
{
    public class Experience
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string? Company { get; set; }
        [Required]
        public int YOE { get; set; }
        [Required]
        public string? Role { get; set; }

    }
}
