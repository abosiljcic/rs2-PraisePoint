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
                    CompanyId = "226255c7-3eff-430d-9d01-89ec49bf44c5"
                },
                new Points()
                {
                    UserName = "602d2149e773f2a3990b47f5",
                    ReceivedPoints = 300,
                    Budget = 500,
                    CompanyId = "48f8658e-69c2-4dac-9c36-e58749d8ec13"
                },
                new Points()
                {
                    UserName = "602d2149e773f2a3990b47fa",
                    ReceivedPoints = 20,
                    Budget = 100,
                    CompanyId = "cbce92ae-f727-40af-86c8-8ea60c752632"
                }
            };


        }
    }
}
