using Posts.Domain.Entities;

namespace Posts.Application.Contracts.Infrastructure;

public interface IUserService
{
    Task<UserInfoDto?> GetUserInfo(string username);
}