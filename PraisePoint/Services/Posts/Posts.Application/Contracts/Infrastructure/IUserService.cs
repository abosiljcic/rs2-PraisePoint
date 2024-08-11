using Posts.Domain.Entities;

namespace Posts.Application.Contracts.Infrastructure;

public interface IUserService
{
    Task<UserInfo?> GetUserInfo(string username);
}