using Microsoft.AspNetCore.Identity;

namespace DREAMCatcher.Models
{
    public class ApplicationRole : IdentityRole
    {
        
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
