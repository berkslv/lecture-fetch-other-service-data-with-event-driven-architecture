using NSwag;
using Organizations.API;
using Organizations.Application;
using Organizations.Infrastructure;
using Serilog;
using Shared.Filters.Correlations;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

builder.Services.AddAPIServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHealthChecks("/health");

app.UseMiddleware<CorrelationMiddleware>();

app.UseSerilogRequestLogging();

app.UseOpenApi(configure => configure.PostProcess = (document, _) => document.Schemes = new List<OpenApiSchema>(){ NSwag.OpenApiSchema.Https });

app.UseSwaggerUi();

app.UseExceptionHandler();

app.MapControllers();

await app.RunAsync();

