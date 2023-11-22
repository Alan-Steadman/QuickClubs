using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Domain.Memberships.Repository;
public interface IMembershipRepository
{
    Task<Membership?> GetByIdAsync(MembershipId id, CancellationToken cancellationToken = default);
    void Add(Membership membershipOption);
    void Update(Membership membershipOption);
}
