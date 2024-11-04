using KindPaws.WEB;
using KindPaws.WEB.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// lower case routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add logging and all modules dependencies
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddSpeciesModule();
builder.Services.AddVolunteersModule(builder.Configuration);
builder.Services.AddApplicationLayers();

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
app.UseAuthorization();
app.MapControllers();
app.Run();