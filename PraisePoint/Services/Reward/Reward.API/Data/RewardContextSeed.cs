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
                    user_id = "602d2149e773f2a3990b47f8",
                    received_points = 120,
                    budget = 300,
                    company_id = "602d2149e773f2a3990b47f9"
                },
                new Points()
                {
                    user_id = "602d2149e773f2a3990b47f5",
                    received_points = 300,
                    budget = 500,
                    company_id = "602d2149e773f2a3990b47f6"
                },
                new Points()
                {
                    user_id = "602d2149e773f2a3990b47fa",
                    received_points = 20,
                    budget = 100,
                    company_id = "602d2149e773f2a3990b47fb"
                }
            };


        }
    }
}
