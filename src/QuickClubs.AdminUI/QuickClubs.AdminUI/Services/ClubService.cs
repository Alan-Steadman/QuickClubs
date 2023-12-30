using QuickClubs.AdminUI.Extensions;
using QuickClubs.Contracts.Clubs;

namespace QuickClubs.AdminUI.Services;

public class ClubService : IClubService
{

    private readonly ApiService _apiService;

    public ClubService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<ClubResponse>?> GetAllClubs()
    {
        await _apiService.SetAuthorizationHeader();
        var content = await _apiService.HttpClient.GetFromJsonAsync<IEnumerable<ClubResponse>>("api/clubs");
        return content;
    }

    public async Task<ClubResponse?> GetClub(Guid id)
    {
        await _apiService.SetAuthorizationHeader();
        var content = await _apiService.HttpClient.GetFromJsonAsync<ClubResponse>($"api/clubs/{id}");
        return content;
    }

    public async Task<Guid> CreateClub(CreateClubRequest club)
    {
        await _apiService.SetAuthorizationHeader();
        var response = await _apiService.HttpClient.PostAsJsonAsync<CreateClubRequest>("api/clubs", club);
        return await response.ToResult<Guid>();
    }
}
