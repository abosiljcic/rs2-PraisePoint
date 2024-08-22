using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Infrastructure;
using Posts.Application.Contracts.Persistence;
using Posts.Infrastructure.Factories;
using Posts.Infrastructure.Persistence;
using Posts.Infrastructure.Repositories;
using Posts.Infrastructure.Services;

namespace Posts.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PostContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PostConnectionString")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IPostRepository, PostRepository>();

        services.AddScoped<IPostFactory, PostFactory>();
        services.AddScoped<IPostViewModelFactory, PostViewModelFactory>();

        services.AddScoped<IUserService, UserService>();
        services.AddHttpClient<IUserService, UserService>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetConnectionString("UserServiceUriString") ?? string.Empty);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.GetSection("JwtTokens")["UserService"]);
        });

        return services;
    }
}