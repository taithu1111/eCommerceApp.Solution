using Microsoft.AspNetCore.Identity;
namespace eCommerceApp.Domain.Entities.Identtity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
