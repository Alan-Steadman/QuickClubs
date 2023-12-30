using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using QuickClubs.Contracts.Constants.Storage;
using System.Net.Http.Headers;

namespace QuickClubs.AdminUI.Services;

public class ApiService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    public readonly HttpClient _httpClient;

    public ApiService(ProtectedSessionStorage sessionStorage, HttpClient httpClient)
    {
        _sessionStorage = sessionStorage;
        _httpClient = httpClient;
    }

    public async Task SetAuthorizationHeader()
    {
        var savedTokenResult = await _sessionStorage.GetAsync<string>(StorageConstants.Local.AuthToken);
        var savedToken = savedTokenResult.Success ? savedTokenResult.Value : null;

        if (!string.IsNullOrWhiteSpace(savedToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }
    }
}
