using System.ComponentModel.DataAnnotations;

namespace DREAMCatcher.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Please Enter Username")]
        public string? Username { get; set; }
        [Required(ErrorMessage ="Please Enter Password")]
        public string? Password { get; set; }
    }
}
