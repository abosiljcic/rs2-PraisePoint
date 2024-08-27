using System.Security.Claims;

namespace User.API.Services
{
    public interface IUserService
    {
        Task<Entities.User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<Entities.User> GetUserById(String userId);
        Task<int> GetCompanyPointsNumber(Guid companyId);

        Task<List<Entities.Company>> GetCompanies();
    }
}
