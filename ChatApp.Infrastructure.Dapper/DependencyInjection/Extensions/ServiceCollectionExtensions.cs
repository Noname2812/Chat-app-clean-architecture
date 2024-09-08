using ChatApp.Domain.Abstractions.Dappers;
using ChatApp.Infrastructure.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Infrastructure.Dapper.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureDapper(this IServiceCollection services)
        => services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>()
            .AddScoped<RoomChatQueryRepository>();
    }
}
