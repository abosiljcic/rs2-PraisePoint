using MongoDB.Driver;
using Reward.API.Entities;

namespace Reward.API.Data
{
    public class RewardContextSeed
    {
        public static void SeedData(IMongoCollection<Points> pointsCollection)
        {
            var existPoints = pointsCollection.Find(p => true).Any(); //da li postoji neki points
            if (!existPoints)
            {
                pointsCollection.InsertManyAsync(GetPreconfiguredPoints());
            }

        }

        private static IEnumerable<Points> GetPreconfiguredPoints()
        {
            return new List<Points>()
            {
                new Points()
                {
                    UserName = "ilija",
                    ReceivedPoints = 0,
                    Budget = 5000,
                    CompanyId = "18809c4c-f5d3-421a-9a4e-0ac08b247352"
                },
                new Points()
                {
                    UserName = "nikolina",
                    ReceivedPoints = 0,
                    Budget = 5000,
                    CompanyId = "18809c4c-f5d3-421a-9a4e-0ac08b247352"
                },
                new Points()
                {
                    UserName = "andrijana",
                    ReceivedPoints = 0,
                    Budget = 5000,
                    CompanyId = "18809c4c-f5d3-421a-9a4e-0ac08b247352"
                }
            };

        }
    }
}
