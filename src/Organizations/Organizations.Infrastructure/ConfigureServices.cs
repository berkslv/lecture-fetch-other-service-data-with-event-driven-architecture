using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizations.Application.Interfaces;
using Organizations.Infrastructure.Persistence;
using Organizations.Infrastructure.Persistence.Interceptors;
using Products.Infrastructure.Services;
using Refit;
using Shared.Filters.Correlations;

namespace Organizations.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.RegisterHttpClients(configuration);
        
        return services;
    }

    private static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<CorrelationHeaderHandler>();

        var httpServices = configuration.GetSection("HttpClients");
        var parameterUrl = httpServices["Products"]!;

        services
            .AddRefitClient<IProductAPI>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(parameterUrl))
            .AddHttpMessageHandler<CorrelationHeaderHandler>()
            .AddStandardResilienceHandler();
        services.AddScoped<IProductService>(provider => provider.GetRequiredService<IProductAPI>());
    }
}