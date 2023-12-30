
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace QuickClubs.AdminUI.Authentication;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly ProtectedSessionStorage _sessionStorage;

    public AuthenticationHandler(ProtectedSessionStorage sessionStorage)
        => _sessionStorage = sessionStorage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Code below should work but doesn't due to issue https://github.com/dotnet/aspnetcore/issues/40336

        //if (request.Headers.Authorization?.Scheme != "Bearer")
        //{
        //    var savedTokenResult = await _sessionStorage.GetAsync<string>(StorageConstants.Local.AuthToken);
        //    var savedToken = savedTokenResult.Success ? savedTokenResult.Value : null;

        //    if (!string.IsNullOrWhiteSpace(savedToken))
        //    {
        //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        //    }
        //}

        return await base.SendAsync(request, cancellationToken);
    }
}
