using ChatApp.Application.Abstractions.Services;
using ChatApp.Infrastructure.Authentication;
using ChatApp.Infrastructure.Caching;
using ChatApp.Infrastructure.Cloudinary;
using ChatApp.Infrastructure.DependencyInjection.Options;
using ChatApp.Infrastructure.Hubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ChatApp.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        // add jwt service
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        // add cloudinary service
        services.AddTransient<ICloudinaryService, CloudinaryService>();

    }
    public static void AddConfigurationRedis(this IServiceCollection services, IConfiguration configuration)
    {
        RedisOptions redisOption = new RedisOptions();
            configuration.GetSection("RedisOptions").Bind(redisOption);
        services.AddStackExchangeRedisCache(options => options.Configuration = redisOption.ConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisOption.ConnectionString));
        services.AddTransient<IRedisService, RedisService>();
    }
    public static void AddConfigurationSignalR(this IServiceCollection services) 
    {
        services.AddSignalR();
        services.AddTransient<IHubService, HubService>();
    }
}