using System.Text.Json;
using Microsoft.Extensions.Logging;
using Posts.Application.Contracts.Infrastructure;
using Posts.Domain.Entities;

namespace Posts.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserService> _logger;

    public UserService(HttpClient httpClient, ILogger<UserService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UserInfoDto?> GetUserInfo(string username)
    {
        var response = await _httpClient.GetAsync($"/api/v1/User/{username}");
        
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return new UserInfoDto();
        }

        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation($"User info response : {content}");
        return JsonSerializer.Deserialize<UserInfoDto>(content, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
    }
}