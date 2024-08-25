using Shared.Filters.Correlations.Models;

namespace Shared.Filters.Correlations;

public class CorrelationHeaderHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var correlation = AsyncStorage<Correlation>.Retrieve();

        if (correlation is not null)
        {
            request.Headers.Add("CorrelationId", correlation.Id.ToString());
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
