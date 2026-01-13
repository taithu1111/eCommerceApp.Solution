using eCommerceApp.Infrastructure.DependencyInject;
using eCommerceApp.Application.DependencyInject;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building....");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(options =>
    {
        options.AllowAnyHeader()
               .AllowAnyOrigin()
               .AllowCredentials();
    });
});

try
{
    var app = builder.Build();
    app.UseCors();
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseInfrastructureServices();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();   
    Log.Logger.Information("Application is running....");
    app.Run();
}catch (Exception ex)
{
    Log.Logger.Error(ex, "Application failed to start...");
}
finally
{
    Log.CloseAndFlush();
}
