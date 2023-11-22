using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Memberships.CreateMembership;

public sealed record CreateMembershipCommand(
    UserId UserId,
    List<UserId> AdditionalMembers,
    MembershipOptionId MembershipOptionId,
    MembershipLevelId MembershipLevelId) : ICommand<Guid>;