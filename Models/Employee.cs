using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.Xml;

namespace DREAMCatcher.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        
        public int UserId { get; set; }
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
        [AllowNull]
        public string? Image { get; set; }
        [Required]
        public string? Resume {  get; set; }
        [Required]
        public string? Link { get; set; }
        [Required]
        public string? Git { get; set; }
        [Required]
        public string? DOJ { get; set;}

    }
}
