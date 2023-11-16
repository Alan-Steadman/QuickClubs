using QuickClubs.Domain.Clubs.ValueObjects;

namespace QuickClubs.Domain.Clubs.Repository;

public interface IClubRepository
{
    Task<Club?> GetByIdAsync(ClubId id, CancellationToken cancellationToken = default);
    void Add(Club club);
    void Update(Club club);

    Task<bool> IsFullNameUniqueAsync(ClubId? id, string fullName, CancellationToken cancellationToken = default);
    Task<bool> IsAcronymUniqueAsync(ClubId? id, string acronym, CancellationToken cancellationToken = default);
    Task<bool> IsWebsiteUniqueAsync(ClubId? id, string websiteUrl, CancellationToken cancellationToken = default);
}
