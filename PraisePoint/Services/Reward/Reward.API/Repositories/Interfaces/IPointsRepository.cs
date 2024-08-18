using Reward.API.Entities;

namespace Reward.API.Repositories.Interfaces
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Points>> GetAllPoints();
        Task<Points> GetPointsForUserByUsername(string username);
        Task<IEnumerable<Points>> GetPointsForCompanyById(string companyId);
        Task<bool> UpdateUserAsync(Points user);
        Task<bool> AddAsync(Points newPoints);
    }
}
