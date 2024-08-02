using Reward.API.Entities;

namespace Reward.API.Repositories.Interfaces
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Points>> GetAllPoints();
        Task<Points> GetPointsForUserById(string user_id);
        Task<IEnumerable<Points>> GetPointsForCompanyById(string company_id);
    }
}
