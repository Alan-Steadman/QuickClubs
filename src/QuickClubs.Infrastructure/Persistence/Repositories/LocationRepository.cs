using Microsoft.EntityFrameworkCore;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Locations;
using QuickClubs.Domain.Locations.Repository;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Repositories;

internal sealed class LocationRepository 
    : Repository<Location, LocationId>, ILocationRepository
{
    public LocationRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsNameUniqueAsync(LocationId? id, ClubId clubId, string name, CancellationToken cancellationToken = default)
    {
        var locationName = new LocationName(name);

        return !await DbContext
            .Set<Location>()
            .AnyAsync(location => location.Name == locationName, cancellationToken);
    }
}
