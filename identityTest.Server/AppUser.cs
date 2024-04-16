using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace identityTest.Server
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set;}

        [Required]
        public string? LastName { get; set; }

        public virtual ContactInfo? ContactInfo { get; set;}
    }
}
