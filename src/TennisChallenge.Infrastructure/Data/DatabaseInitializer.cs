using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TennisChallenge.Infrastructure.Data;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            bool result = await dbContext.Database.EnsureCreatedAsync(cancellationToken);
            if (!result)
            {
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
            else
            {
                return;
            }
        }
        catch
        {


        }


    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}