using MongoDB.Driver;
using Reward.API.Entities;

namespace Reward.API.Data
{
    public class RewardContext : IRewardContext
    {
        public RewardContext()
        { 
            var client = new MongoClient("mongodb://rewarddb:27017");
            var database = client.GetDatabase("RewardDB");

            AllPoints = database.GetCollection<Points>("AllPoints");
            RewardContextSeed.SeedData(AllPoints);
        }

        public IMongoCollection<Points> AllPoints { get; }
    }
}
