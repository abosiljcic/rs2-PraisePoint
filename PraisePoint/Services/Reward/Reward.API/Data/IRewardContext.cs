using MongoDB.Driver;
using Reward.API.Entities;

namespace Reward.API.Data
{
    public interface IRewardContext
    {
        IMongoCollection<Points> AllPoints { get; }
    }
}
