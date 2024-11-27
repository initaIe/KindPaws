using DotNetEnv;
using KindPaws.WEB.DI;
using KindPaws.WEB.DI.Injections.Others;
using KindPaws.WEB.Middlewares;
using Serilog;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Adding all dependencies
builder.Services.AddAllDependencies(builder.Configuration);

var app = builder.Build();

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