using System.ComponentModel.DataAnnotations;

namespace DREAMCatcher.Models
{
    public class ProfilePicVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
