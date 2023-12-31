
namespace QuickClubs.AdminUI.Logging;

public class RequestLoggingHandler : DelegatingHandler
{
    private readonly ILogger<RequestLoggingHandler> _logger;

    public RequestLoggingHandler(ILogger<RequestLoggingHandler> logger) 
        => _logger = logger;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending Request: {@request}", request);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Request Succeeded: {@request}", request);
        }
        else
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    _logger.LogWarning("Unauthorized");
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    var error = response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Bad Request>: {@error}", error);
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    _logger.LogError("Internal Server Error");
                    break;

            }
        }

        return response;
    }
}
