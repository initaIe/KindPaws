using DotNetEnv;
using KindPaws.Auth.Presentation.DI;
using KindPaws.Auth.Presentation.Middleware;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Adding all dependencies
builder.Services.AddAuthDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Using all middlewares
app.UseAuthMiddlewares();
app.Run();