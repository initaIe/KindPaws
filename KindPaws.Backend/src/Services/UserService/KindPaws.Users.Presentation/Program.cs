using DotNetEnv;
using KindPaws.Users.Presentation.DI;
using KindPaws.Users.Presentation.Middleware;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// var checkCgf = builder.Configuration;

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