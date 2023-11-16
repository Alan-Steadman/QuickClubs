using Microsoft.EntityFrameworkCore;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Clubs.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Repositories;

internal sealed class ClubRepository : Repository<Club, ClubId>, IClubRepository
{
    public ClubRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsAcronymUniqueAsync(ClubId? id, string acronym, CancellationToken cancellationToken = default)
    {
        return !await DbContext
            .Set<Club>()
            .AnyAsync(club => club.Name.Acronym == acronym, cancellationToken);
    }

    public async Task<bool> IsFullNameUniqueAsync(ClubId? id, string fullName, CancellationToken cancellationToken = default)
    {
        return !await DbContext
            .Set<Club>()
            .AnyAsync(club => club.Name.FullName == fullName, cancellationToken);
    }

    public async Task<bool> IsWebsiteUniqueAsync(ClubId? id, string websiteUrl, CancellationToken cancellationToken = default)
    {
        return !await DbContext
            .Set<Club>()
            .AnyAsync(club => club.Website.Url == websiteUrl, cancellationToken);
    }
}
