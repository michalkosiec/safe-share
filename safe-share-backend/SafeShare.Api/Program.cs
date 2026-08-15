using Microsoft.EntityFrameworkCore;
using SafeShare.Infrastructure.Persistence;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine();

builder.Services.AddOpenApi();



builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();