using TennisChallenge.Application;
using TennisChallenge.Core;
using TennisChallenge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCore();

var app = builder.Build();

app.UseInfrastructure();

app.Run();

public partial class Program
{
    
}