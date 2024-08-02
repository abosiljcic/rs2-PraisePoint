using Reward.API.Data;
using Reward.API.Entities;
using MongoDB.Driver;

namespace Reward.API.Repositories.Interfaces
{
    public class PointsRepository : IPointsRepository
    {
        private readonly IRewardContext _context;

        public PointsRepository(IRewardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Points>> GetAllPoints()
        {
            return await _context.AllPoints.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Points>> GetPointsForCompanyById(string company_id)
        {
            return await _context.AllPoints.Find(p => p.company_id == company_id).ToListAsync();
        }

        public async Task<Points> GetPointsForUserById(string user_id)
        {
            return await _context.AllPoints.Find(p => p.user_id == user_id).FirstOrDefaultAsync();
        }
    }
}
