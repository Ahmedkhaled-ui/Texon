using Microsoft.AspNetCore.Identity;

namespace Texon.Domin.Entities.Auth
{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Address Address { get; set; }
    }
}
