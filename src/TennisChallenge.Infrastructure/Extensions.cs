using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using TennisChallenge.Infrastructure.Data;
using TennisChallenge.Infrastructure.Exceptions;

namespace TennisChallenge.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddSingleton<ExceptionMiddleware>();

        services.AddProblemDetails();

        services.AddOpenApiDocument(options =>
        {
            options.PostProcess = document =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tennis API",
                    Description = "An ASP.NET Core Web API for managing tennis stuff",
                    TermsOfService = "https://example.com/terms",
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = "https://example.com/contact"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = "https://example.com/license"
                    }
                };
            };
        });

        services.AddPostgresDependencies(configuration);

        services.AddAuthentication();

        services.AddAuthorization();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseOpenApi();

        app.UseSwagger();

        app.UseReDoc(x => { x.Path = "/redoc"; });

        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(e 
            => e.MapControllers());
        return app;
    }
}