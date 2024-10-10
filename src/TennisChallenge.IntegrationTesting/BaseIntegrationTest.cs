using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Infrastructure.Data;

namespace TennisChallenge.IntegrationTesting;

public abstract class BaseIntegrationTest 
    : IClassFixture<IntegrationTestWebAppFactory>
{
    internal readonly IServiceScope serviceScope;
    internal readonly ApplicationDbContext DbContext;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        serviceScope = factory.Services.CreateScope();
        DbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}