using eCommerceApp.Domain.Entities.Identtity;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface IRoleManagement
    {
        Task<string?> GetUserRole(string userEmail);
        Task<bool> AddUserToRole(AppUser user, string roleName);
    }
}
