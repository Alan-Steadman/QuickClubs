
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
        if (request.Headers.Authorization?.Scheme != "Bearer")
        {
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI3OGE0Njc0MC1lYTU0LTQzZTAtOTRkNy0yOTgxMDYxNWZjNGMiLCJlbWFpbCI6ImhlbnJ5QGludGVybmV0LmNvbSIsImdpdmVuX25hbWUiOiJoZW5yeSIsImZhbWlseV9uYW1lIjoidHVkb3IiLCJqdGkiOiJkOGI2OWM0ZC00NDJjLTRkYzItOWU1ZC1jOWQyMTc4M2Y0YTAiLCJleHAiOjE3MDM4NDk0MTYsImlzcyI6InF1aWNrY2x1YnMiLCJhdWQiOiJxdWlja2NsdWJzIn0.4N1CPfgc8Uh0xY4zO2xJ0dXaEFAq2HnAWuLtv7aW0vQ");

            var savedTokenResult = await _sessionStorage.GetAsync<string>(StorageConstants.Local.AuthToken);
            var savedToken = savedTokenResult.Success ? savedTokenResult.Value : null;

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
