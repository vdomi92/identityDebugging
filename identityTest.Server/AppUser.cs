using Microsoft.AspNetCore.Identity;

namespace identityTest.Server
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set;}

        public string? LastName { get; set; }

        public virtual ContactInfo? ContactInfo { get; set;}
    }
}
