using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Memberships.Common;

namespace QuickClubs.Application.Memberships.CreateMembership;

public sealed record CreateMembershipCommand(
    Guid UserId,
    List<Guid> AdditionalMembers,
    Guid MembershipOptionId,
    Guid MembershipLevelId) : ICommand<MembershipResult>;