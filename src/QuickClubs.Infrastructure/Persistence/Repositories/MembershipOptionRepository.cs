using QuickClubs.Domain.MembershipOptions.Repository;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.MembershipOptions;
using Microsoft.EntityFrameworkCore;

namespace QuickClubs.Infrastructure.Persistence.Repositories;

internal sealed class MembershipOptionRepository : Repository<MembershipOption, MembershipOptionId>, IMembershipOptionRepository
{
    public MembershipOptionRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsNameUniqueAsync(MembershipOptionId? id, string name, CancellationToken cancellationToken = default)
    {
        return !await DbContext
            .Set<MembershipOption>()
            .AnyAsync(mo => mo.Name.Value == name, cancellationToken);
    }
}
