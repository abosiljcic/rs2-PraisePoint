using System.Security.Claims;
using User.API.Entities;

namespace User.API.Services
{
    public interface IUserService
    {
        Task<Entities.User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<Entities.User> GetUserById(String userId);
        Task<Company> GetCompanyPointsNumber(Guid companyId);

        Task<List<Entities.Company>> GetCompanies();
    }
}
