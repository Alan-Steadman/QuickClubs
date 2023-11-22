using QuickClubs.Domain.Memberships;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Repositories;

internal sealed class MembershipRepository : Repository<Membership, MembershipId>, IMembershipRepository
{
    public MembershipRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
