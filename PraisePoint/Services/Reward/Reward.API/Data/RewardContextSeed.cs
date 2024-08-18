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
                    UserName = "602d2149e773f2a3990b47f8",
                    ReceivedPoints = 120,
                    Budget = 300,
                    CompanyId = "602d2149e773f2a3990b47f9"
                },
                new Points()
                {
                    UserName = "602d2149e773f2a3990b47f5",
                    ReceivedPoints = 300,
                    Budget = 500,
                    CompanyId = "602d2149e773f2a3990b47f6"
                },
                new Points()
                {
                    UserName = "602d2149e773f2a3990b47fa",
                    ReceivedPoints = 20,
                    Budget = 100,
                    CompanyId = "602d2149e773f2a3990b47fb"
                }
            };


        }
    }
}
