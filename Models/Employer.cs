using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DREAMCatcher.Models
{
    public class Employer
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? Company_Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? DOJ { get; set; }
        [Required]
        public string? Locations { get; set; }
        [AllowNull]
        public string Logo { get; set; }
    }
}
