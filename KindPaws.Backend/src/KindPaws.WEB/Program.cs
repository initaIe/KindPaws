using DotNetEnv;
using KindPaws.Accounts.Infrastructure.Seeding;
using KindPaws.WEB.DI;
using KindPaws.WEB.DI.Injections.Others;
using KindPaws.WEB.Middlewares;
using Serilog;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// lower case routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

// Adding all dependencies
builder.Services.AddAllDependencies(builder.Configuration);

var app = builder.Build();

// TODO refactor
var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();
await accountsSeeder.SeedAsync();

// Add exception middleware
app.UseMiddlewareException();
// Serilog
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();