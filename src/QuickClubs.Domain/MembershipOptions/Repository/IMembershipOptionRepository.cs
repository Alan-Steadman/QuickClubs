using QuickClubs.Domain.MembershipOptions.ValueObjects;

namespace QuickClubs.Domain.MembershipOptions.Repository;
public interface IMembershipOptionRepository
{
    Task<MembershipOption?> GetByIdAsync(MembershipOptionId id, CancellationToken cancellationToken = default);

    void Add(MembershipOption membershipOption);

    Task<bool> IsNameUniqueAsync(MembershipOptionId? id, string name, CancellationToken cancellationToken = default);
}
