using System.Text.Json;
using Microsoft.Extensions.Logging;
using Posts.Application.Contracts.Infrastructure;
using Posts.Domain.Entities;

namespace Posts.Infrastructure.Services;

public class RewardService : IRewardService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserService> _logger;

    public RewardService(HttpClient httpClient, ILogger<UserService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PointsInfoDto?> GetPointsForUser(string username)
    {
        _logger.LogInformation($"GetPointsForUser({username})");
        var response = await _httpClient.GetAsync($"/users/{username}");
        
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation($"PointsInfo for user : {username} : {content}");
        PointsInfoDto pointsInfoDto = JsonSerializer.Deserialize<PointsInfoDto>(content, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        return pointsInfoDto;
    }
}