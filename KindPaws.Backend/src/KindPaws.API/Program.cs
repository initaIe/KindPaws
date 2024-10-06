using KindPaws.API.Extensions;
using KindPaws.API.Middlewares;
using KindPaws.Application.Extensions;
using KindPaws.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                 ?? throw new ArgumentNullException("Seq connection string not found"))
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();

builder.Services // add all layers dependencies
    .AddInfrastructure()
    .AddApi()
    .AddApplication();

var app = builder.Build();

app.UseExceptionMiddleware(); // ex middleware

app.UseSerilogRequestLogging(); // serilog

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // await app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();