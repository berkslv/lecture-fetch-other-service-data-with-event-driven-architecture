using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Products.Application.Behaviours;

public class LoggingResponseBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingResponseBehaviour<TRequest, TResponse>> _logger;

    public LoggingResponseBehaviour(ILogger<LoggingResponseBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        var responseName = typeof(TResponse).Name;
        var username = "system";

        _logger.LogInformation("Sample Response: {ResponseName} {@Response} {@Username}", responseName, response, username);

        return Task.CompletedTask;
    }
}
