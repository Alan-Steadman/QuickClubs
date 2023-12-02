using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.Memberships.CreateMembership;

public sealed record CreateMembershipCommand(
    Guid UserId,
    List<Guid> AdditionalMembers,
    Guid MembershipOptionId,
    Guid MembershipLevelId) : ICommand<Guid>;