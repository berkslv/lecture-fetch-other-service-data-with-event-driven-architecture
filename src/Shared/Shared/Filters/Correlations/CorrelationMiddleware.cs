using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Serilog.Events;
using Shared.Filters.Correlations.Models;

namespace Shared.Filters.Correlations;

public class CorrelationMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationIdHeader = context.Request.Headers["CorrelationId"];

        if (!string.IsNullOrWhiteSpace(correlationIdHeader))
        {
            var correlationId = Guid.Parse(correlationIdHeader.ToString());

            LogContext.PushProperty("CorrelationId", new ScalarValue(correlationId));

            AsyncStorage<Correlation>.Store(new Correlation
            {
                Id = correlationId
            });
        }

        await _next(context);
    }
}