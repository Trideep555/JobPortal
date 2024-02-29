using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DREAMCatcher.Models
{
    public class EmployerVM
    {
        public int Id { get; set; }
        [Required]
        public string? Email {  get; set; }
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
        public IFormFile Logo { get; set; }
    }
}
