using QuickClubs.Contracts.Clubs;

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
}
