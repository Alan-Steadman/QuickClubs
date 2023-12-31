using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using QuickClubs.AdminUI.Authentication;
using QuickClubs.Contracts.Authentication;
using QuickClubs.Contracts.Constants.Routes;
using QuickClubs.Contracts.Constants.Storage;
using System.Net.Http.Headers;

namespace QuickClubs.AdminUI.Services;

public class AuthenticationService : IAuthenticationService
{

    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        ProtectedSessionStorage sessionStorage,
        AuthenticationStateProvider authenticationStateProvider,
        HttpClient httpClient,
        ILogger<AuthenticationService> logger)
    {
        _sessionStorage = sessionStorage;
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsync(AuthenticationEndpoints.LoginEndpoint, JsonContent.Create(request));

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Login failed for {@Email}.  Response was {@StatusCode}", request.Email, response.StatusCode);
            throw new UnauthorizedAccessException("Login failed.");
        }

        var content = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (content == null)
        {
            _logger.LogWarning("Login failed unexpectedly.  Login responded with success status code but could not get valid AuthenticationResponse from login response. {@Email}", request.Email);
            throw new InvalidDataException();
        }

        await _sessionStorage.SetAsync(StorageConstants.Local.AuthToken, content.Token);

        await ((AppAuthenticationStateProvider)_authenticationStateProvider).StateChangedAsync();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", content.Token);

        _logger.LogInformation("Login success: {@Email}", content.Email);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _sessionStorage.DeleteAsync(StorageConstants.Local.AuthToken);
        ((AppAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
