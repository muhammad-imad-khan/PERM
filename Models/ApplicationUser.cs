using Microsoft.AspNetCore.Identity;

namespace PERM.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? ProfilePicture { get; set; }

    }
}
