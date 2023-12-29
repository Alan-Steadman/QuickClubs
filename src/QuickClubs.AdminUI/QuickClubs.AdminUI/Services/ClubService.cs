using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using QuickClubs.Contracts.Constants.Storage;
using System.Net.Http.Headers;

using QuickClubs.AdminUI.Extensions;
using QuickClubs.Contracts.Clubs;
using System.Net.Http;
using System.Net.Http.Json;

namespace QuickClubs.AdminUI.Services;

public class ClubService : IClubService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly HttpClient _httpClient;

    public ClubService(ProtectedSessionStorage sessionStorage, HttpClient httpClient)
    {
        _sessionStorage = sessionStorage;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ClubResponse>?> GetAllClubs()
    {
        //var savedTokenResult = await _sessionStorage.GetAsync<string>(StorageConstants.Local.AuthToken);
        //var savedToken = savedTokenResult.Success ? savedTokenResult.Value : null;

        //if (!string.IsNullOrWhiteSpace(savedToken))
        //{
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        //}

        var content = await _httpClient.GetFromJsonAsync<IEnumerable<ClubResponse>>("");
        return content;
    }

    public async Task<ClubResponse?> GetClub(Guid id)
    {
        var content = await _httpClient.GetFromJsonAsync<ClubResponse>($"clubs/{id}");
        return content;
    }

    public async Task<Guid> CreateClub(CreateClubRequest club)
    {
        var response = await _httpClient.PostAsJsonAsync<CreateClubRequest>("clubs", club);
        return await response.ToResult<Guid>();
    }
}
