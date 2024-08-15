using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using User.API.Data;

namespace User.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _dbContext;

        public UserService(UserContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Entities.User> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            var username = principal.FindFirstValue(ClaimTypes.Name) ?? principal.FindFirst("name")?.Value;
            var email = principal.FindFirstValue(ClaimTypes.Email) ?? principal.FindFirst("email")?.Value;

            if (!string.IsNullOrEmpty(username))
            {
                return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            }
            else if (!string.IsNullOrEmpty(email))
            {
                return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            }

            return null;
        }

        public async Task<Entities.User> GetUserById(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }
        public async Task<int> GetCompanyPointsNumber(Guid companyId)
        {
            var pointsNumber = await _dbContext.Companies
                    .Where(s => s.Id == companyId)
                    .Select(s => s.PointsNumber)
                    .FirstOrDefaultAsync();

            return pointsNumber;
        }
    }
}
