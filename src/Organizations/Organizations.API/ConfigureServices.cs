using System.Reflection;
using Elasticsearch.Net;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Organizations.API.Filters;
using Organizations.Infrastructure.Persistence;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Shared.Filters.Correlations;

namespace Organizations.API;

public static class ConfigureServices
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<ExceptionHandleMiddleware>();
        services.AddProblemDetails();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers();


        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument((configure, serviceProvider) =>
        {
            configure.Title = "Organizations API";
        });

        services.RegisterMassTransitServices(configuration);

        return services;
    }

    private static IServiceCollection RegisterMassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBroker = configuration.GetSection("MessageBroker");
        services.AddMassTransit(cfg =>
        {
            cfg.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(5);
                o.UseSqlite().UseBusOutbox();
            });

            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddConsumers(Assembly.GetExecutingAssembly());

            cfg.UsingRabbitMq((context, config) =>
            {
                config.UseSendFilter(typeof(CorrelationSendFilter<>), context);
                config.UsePublishFilter(typeof(CorrelationPublishFilter<>), context);
                config.UseConsumeFilter(typeof(CorrelationConsumeFilter<>), context);

                config.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(5)));

                config.Host(messageBroker["Host"], messageBroker["VirtualHost"], h =>
                {
                    h.Username(messageBroker["Username"]!);
                    h.Password(messageBroker["Password"]!);
                });

                config.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        var elasticSearch = builder.Configuration.GetSection("ElasticSearch");
        var projectName = builder.Configuration.GetValue<string>("ProjectName");
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var index = $"{projectName}-{DateTime.Today:yyyyMMdd}";
        Log.Logger = new LoggerConfiguration()
            .Enrich
            .FromLogContext()
            .Enrich
            .WithProperty("ApplicationName", $"{projectName} - {environment}")
            .WriteTo
            .Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(elasticSearch["Url"]!))
                {
                    IndexFormat = index,
                    AutoRegisterTemplate = true,
                    DetectElasticsearchVersion = true
                })
            .ReadFrom
            .Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();

        builder.Host.UseSerilog(Log.Logger, true);

        return builder;
    }
}