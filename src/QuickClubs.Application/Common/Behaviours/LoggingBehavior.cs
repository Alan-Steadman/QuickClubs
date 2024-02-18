using MediatR;
using QuickClubs.Application.Abstractions.Mediator;
using Microsoft.Extensions.Logging;

namespace QuickClubs.Application.Common.Behaviours;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandName = request.GetType().Name;
        try
        {
            _logger.LogInformation("Executing command {@Command}, {@DateTimeUtc}", commandName, DateTime.UtcNow);

            var result = await next();

            _logger.LogInformation("Command {Command} processed successfully, {@DateTimeUtc}", commandName, DateTime.UtcNow);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Command {Command} processing failed, {@DateTimeUtc} Message: {exceptionMessage}, Stack Trace: {stackTrace}", commandName, DateTime.UtcNow, ex.Message, ex.StackTrace);

            throw;
        }
    }
}
