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

        public Task<bool> AddAsync(Points newPoints)
        {
            try
            {
                // Dodajemo novi dokument u kolekciju
                _context.AllPoints.InsertOne(newPoints);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Logovanje greške ako je potrebno
                Console.WriteLine($"An error occurred while adding points: {ex.Message}");
                return Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<Points>> GetAllPoints()
        {
            return await _context.AllPoints.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Points>> GetPointsForCompanyById(string company_id)
        {
            return await _context.AllPoints.Find(p => p.CompanyId == company_id).ToListAsync();
        }

        public async Task<Points> GetPointsForUserByUsername(string username)
        {
            return await _context.AllPoints.Find(p => p.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUserAsync(Points user)
        {
            var filter = Builders<Points>.Filter.Eq(p => p.UserName, user.UserName);

            var updateDefinition = Builders<Points>.Update
                .Set(p => p.Budget, user.Budget)
                .Set(p => p.ReceivedPoints, user.ReceivedPoints);

            var result = await _context.AllPoints.UpdateOneAsync(filter, updateDefinition);

            return result.ModifiedCount > 0;
        }
    }
}
