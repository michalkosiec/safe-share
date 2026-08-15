using Microsoft.EntityFrameworkCore;
using SafeShare.Application.Features.Users;
using SafeShare.Infrastructure;
using SafeShare.Infrastructure.Persistence;
using Scalar.AspNetCore;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(SafeShare.Application.Features.Users.CreateUserHandler).Assembly);
    
    // ⚡ Używamy wbudowanej metody, która automatycznie cofa restrykcje wersji 6.0
    // i bezbłędnie radzi sobie z lambdami Entity Frameworka:
    opts.RestoreV5Defaults();
});

builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();