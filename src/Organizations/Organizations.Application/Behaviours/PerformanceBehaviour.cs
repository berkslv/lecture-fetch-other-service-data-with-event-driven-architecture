using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Organizations.Application.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<PerformanceBehaviour<TRequest, TResponse>> _logger;

    private readonly Stopwatch _timer = new();

    public PerformanceBehaviour(ILogger<PerformanceBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var username = "system";

            _logger.LogWarning("Sample: Long Running Request {Name} ({ElapsedMilliseconds} milliseconds) {@Username} {@Request}",
                requestName, elapsedMilliseconds, username, request);
        }

        return response;
    }
}
