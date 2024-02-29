using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DREAMCatcher.Models
{
    public class ProfileAboutVM
    {
        public int Id { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
        [Required]
        public string? DOB { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]

        public string? Country { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? Skills { get; set; }
        [Required]
        public IFormFile Resume { get; set; }
        [Required]
        public string? Link { get; set; }
        [Required]
        public string? Git { get; set; }
        
    }
}
