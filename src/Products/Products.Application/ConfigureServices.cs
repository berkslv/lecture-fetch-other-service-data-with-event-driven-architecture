using System.Reflection;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Behaviours;

namespace Products.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                .AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingRequestBehaviour<>))
                .AddRequestPostProcessor(typeof(IRequestPostProcessor<,>), typeof(LoggingResponseBehaviour<,>));
        });

        return services;
    }
}