
using MassTransit;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Shared.Filters.Correlations.Models;
using LogContext = Serilog.Context.LogContext;

namespace Shared.Filters.Correlations;


public class CorrelationConsumeFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly ILogger<CorrelationConsumeFilter<T>> _logger;

    public CorrelationConsumeFilter(ILogger<CorrelationConsumeFilter<T>> logger)
    {
        _logger = logger;
    }

    public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var correlationIdHeader = context.CorrelationId;

        if (correlationIdHeader.HasValue)
        {
            var correlationId = correlationIdHeader.Value;

            LogContext.PushProperty("CorrelationId", new ScalarValue(correlationId));

            AsyncStorage<Correlation>.Store(new Correlation
            {
                Id = correlationId
            });
        }

        _logger.LogInformation("Event {EventType} with content {Event} has been consumed", context.Message.GetType(), context.Message);
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}