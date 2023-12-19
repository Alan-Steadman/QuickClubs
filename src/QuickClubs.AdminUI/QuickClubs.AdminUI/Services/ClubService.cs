using QuickClubs.AdminUI.Extensions;
using QuickClubs.Contracts.Clubs;
using System.Net.Http.Json;

namespace QuickClubs.AdminUI.Services;

public class ClubService : IClubService
{
    private readonly HttpClient _httpClient;

    public ClubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ClubResponse>?> GetAllClubs()
    {
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
