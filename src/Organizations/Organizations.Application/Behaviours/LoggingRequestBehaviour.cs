using System.Text.Json;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Shared.Filters.Correlations.Models;

namespace Organizations.Application.Behaviours;

public class LoggingRequestBehaviour<TRequest>: IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<LoggingRequestBehaviour<TRequest>> _logger;

    public LoggingRequestBehaviour(ILogger<LoggingRequestBehaviour<TRequest>> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var username = "system";

        _logger.LogInformation("Request: {RequestName} {@Request} {@Username}", requestName, request, username);

        return Task.CompletedTask;
    }
}