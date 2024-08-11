using System.Text.Json;
using Posts.Application.Contracts.Infrastructure;
using Posts.Domain.Entities;

namespace Posts.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserInfo?> GetUserInfo(string username)
    {
        var response = await _httpClient.GetAsync($"/api/v1/User/{username}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserInfo>(content);
    }
}