using Microsoft.EntityFrameworkCore;
using SafeShare.Application.Features.Users;
using SafeShare.Infrastructure;
using SafeShare.Infrastructure.Persistence;
using SafeShare.Infrastructure.Storage;
using Scalar.AspNetCore;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();
    opts.Discovery.IncludeAssembly(typeof(SafeShare.Application.Common.AssemblyReference).Assembly);
    opts.RestoreV5Defaults();
});

builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    await dbContext.Database.MigrateAsync();
}

await app.Services.InitializeAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();