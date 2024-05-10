using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using User.API.Data;

namespace User.API.Extensions
{
    public static class UserExtensions
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(options => { options.UseSqlServer(configuration.GetConnectionString("UserConnectionString")); });
            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Entities.User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
