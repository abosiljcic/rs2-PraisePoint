using Posts.Domain.Entities;

namespace Posts.Application.Contracts.Infrastructure;

public interface IRewardService
{
    Task<PointsInfoDto?> GetPointsForUser(string username);
}