using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.Memberships.RejectMembership;
public sealed record RejectMembershipCommand(
    Guid MembershipId,
    Guid RejectedByUserId,
    string Reason) : ICommand;