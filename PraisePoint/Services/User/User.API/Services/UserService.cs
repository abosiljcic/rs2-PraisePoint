using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using User.API.Data;
using User.API.Entities;

namespace User.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(UserContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<Company> GetCompanyPointsNumber(Guid companyId)
        {
            var company = await _dbContext.Companies
                                .Where(s => s.Id == companyId)
                                .FirstOrDefaultAsync<Company>();

            return _mapper.Map<Company>(company);
        }

        public async Task<List<Entities.Company>> GetCompanies()
        {
            var companies = await _dbContext.Companies.ToListAsync();
            return companies;
        }

    }
}
