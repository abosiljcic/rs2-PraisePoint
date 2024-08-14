using System.Security.Claims;

namespace User.API.Services
{
    public interface IUserService
    {
        Task<Entities.User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<Entities.User> GetUserById(String userId);
    }
}
