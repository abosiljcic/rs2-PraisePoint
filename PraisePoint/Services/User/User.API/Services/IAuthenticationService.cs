using User.API.DTOs;

namespace User.API.Services
{
    public interface IAuthenticationService
    {
        Task<Entities.User?> ValidateUser(UserCredentialsDto userCredentials);
        Task<AuthenticationModel> CreateAuthenticationModel(Entities.User user);
        Task RemoveRefreshToken(Entities.User user, string refreshToken);
    }
}
