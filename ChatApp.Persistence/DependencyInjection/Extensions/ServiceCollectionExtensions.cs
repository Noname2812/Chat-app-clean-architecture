

using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Persistence.DependencyInjection.Options;
using ChatApp.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ChatApp.Persistence.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSqlConfiguration(this IServiceCollection services)
        {
            services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var options = provider.GetRequiredService<IOptionsMonitor<SQLServerRetryOptions>>();
                #region ============== SQL-SERVER-STRATEGY-1 ==============

                builder
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true)
                .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
                .UseSqlServer(
                    connectionString: configuration.GetConnectionString("ConnectionStrings"),
                    sqlServerOptionsAction: optionsBuilder
                            => optionsBuilder.ExecutionStrategy(
                                    dependencies => new SqlServerRetryingExecutionStrategy(
                                        dependencies: dependencies,
                                        maxRetryCount: options.CurrentValue.MaxRetryCount,
                                        maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                                        errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

                #endregion ============== SQL-SERVER-STRATEGY-1 ==============

                #region ============== SQL-SERVER-STRATEGY-2 ==============

                //builder
                //.EnableDetailedErrors(true)
                //.EnableSensitiveDataLogging(true)
                //.UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
                //.UseSqlServer(
                //    connectionString: configuration.GetConnectionString("ConnectionStrings"),
                //        sqlServerOptionsAction: optionsBuilder
                //            => optionsBuilder
                //            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

                #endregion ============== SQL-SERVER-STRATEGY-2 ==============
            });

            services.AddIdentityCore<AppUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true; // Default true
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Default 5
                options.Lockout.MaxFailedAccessAttempts = 3; // Default 5
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.AllowedForNewUsers = true;
            });
        }

        public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

        }

        public static OptionsBuilder<SQLServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<SQLServerRetryOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }
}