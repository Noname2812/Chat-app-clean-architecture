using Carter;
using ChatApp.API.DependencyInjection.Extensions;
using ChatApp.API.Middleware;
using ChatApp.Application.DependencyInjections.Extensions;
using ChatApp.Infrastructure.Dapper.DependencyInjection.Extensions;
using ChatApp.Infrastructure.DependencyInjection.Extensions;
using ChatApp.Infrastructure.Hubs;
using ChatApp.Persistence.DependencyInjection.Extensions;
using ChatApp.Persistence.DependencyInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Services.AddInfrastructureServices();

builder.Services
    .AddJwtAuthentication(builder.Configuration);

// Controller API
//builder.Services
//    .AddControllers()
//    .AddApplicationPart(ChatApp.Presentation.AssemblyReference.Assembly);

builder.Host.UseSerilog();

// Add Cater module
builder.Services.AddCarter();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Thay thế với nguồn gốc front-end của bạn
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Cho phép gửi credentials (cookie, auth headers, etc.)
        });
});
// Add configuration
builder.Services.AddConfigureMediatR();
builder.Services.AddConfigureAutoMapper();
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SQLServerRetryOptions)));
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddConfigurationRedis(builder.Configuration);
builder.Services.AddInfrastructureDapper();
builder.Services.AddConfigurationSignalR();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Add Swagger
builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwagger();

// Add middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<BlackListTokenMiddleware>();
var app = builder.Build();

// Using middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.ConfigureSwagger();
    app.UseCors("AllowSpecificOrigin");
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<BlackListTokenMiddleware>();
app.MapCarter();
app.MapHub<ChatHub>("/chat");

//app.MapControllers();
try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhanled exception occured during boostrapping ");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
public partial class Program { }

