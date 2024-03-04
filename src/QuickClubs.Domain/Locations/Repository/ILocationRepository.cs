using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Domain.Locations.Repository;
public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(LocationId id, CancellationToken cancellationToken = default);
    void Add(Location location);
    void Update(Location location);

    Task<bool> IsNameUniqueAsync(LocationId? id, ClubId clubId, string name, CancellationToken cancellationToken = default);
}
