using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.Memberships.ApproveMembership;
public sealed record ApproveMembershipCommand(
    Guid MembershipId,
    Guid ApprovedByUserId,
    string? Reason) : ICommand;