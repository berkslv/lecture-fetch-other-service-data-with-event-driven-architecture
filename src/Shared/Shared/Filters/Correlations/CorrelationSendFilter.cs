using MassTransit;
using Shared.Filters.Correlations.Models;

namespace Shared.Filters.Correlations;

public class CorrelationSendFilter<T> : IFilter<SendContext<T>> where T : class
{
    public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
    {
        var correlation = AsyncStorage<Correlation>.Retrieve();

        if (correlation is not null)
        {
            context.CorrelationId = Guid.Parse(correlation.Id.ToString());
        }

        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}