using QuickClubs.Contracts.Clubs;

namespace QuickClubs.AdminUI.Clubs;

public interface IClubService
{
    Task<IEnumerable<ClubResponse>?> GetAllClubs();
    Task<ClubResponse?> GetClub(Guid id);
}
