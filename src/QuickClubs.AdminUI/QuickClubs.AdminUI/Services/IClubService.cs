using QuickClubs.Contracts.Clubs;

namespace QuickClubs.AdminUI.Services;

public interface IClubService
{
    Task<IEnumerable<ClubResponse>?> GetAllClubs();
    Task<ClubResponse?> GetClub(Guid id);
}
