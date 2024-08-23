using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace User.API.Extensions;

public static class ServiceProviderExtensions
{
    public static WebApplicationBuilder MigrateDatabase<TContext>(this WebApplicationBuilder builder) where TContext : DbContext
    {
        using var scope = builder.Services.BuildServiceProvider().CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, ctx) =>
                    {
                        logger.LogError("Retry {RetryCount} if {PolicyKey} at {OperationKey}, due to {Exception}.", retryCount, ctx.PolicyKey, ctx.OperationKey, exception);
                    });
            retry.Execute(() => context.Database.Migrate());

            logger.LogInformation("Migrating database associated with context {DbContextName} was successful", typeof(TContext).Name);
        } 
        catch (SqlException e)
        {
            logger.LogError(e, "An error occured while migrating the database used on context {DbContextName}", typeof(TContext).Name);
        }

        return builder;
    }
}