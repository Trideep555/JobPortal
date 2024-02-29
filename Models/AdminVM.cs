using System.ComponentModel.DataAnnotations;

namespace DREAMCatcher.Models
{
    public class AdminVM
    {
        [Required(ErrorMessage ="Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirm Password Should Be Same")]
        public string CPassword { get; set; }

    }
}
