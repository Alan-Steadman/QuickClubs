
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using QuickClubs.Contracts.Constants.Storage;
using System.Net.Http.Headers;

namespace QuickClubs.AdminUI.Authentication;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly ProtectedSessionStorage _sessionStorage;

    public AuthenticationHandler(ProtectedSessionStorage sessionStorage)
        => _sessionStorage = sessionStorage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
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
