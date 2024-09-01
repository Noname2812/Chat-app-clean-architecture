

using ChatApp.Application.Behaviors;
using ChatApp.Application.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application.DependencyInjections.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
            => services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly))
            //.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPielineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePielineBehavior<,>))
            //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPielineBehavior<,>))
            //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPielineBehavior<,>))
            .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);
        public static IServiceCollection AddConfigureAutoMapper(this IServiceCollection services)
             => services.AddAutoMapper(typeof(ServiceProfile));
    }
}
