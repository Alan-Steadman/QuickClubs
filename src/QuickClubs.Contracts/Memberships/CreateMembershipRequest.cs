namespace QuickClubs.Contracts.Memberships;

public record CreateMembershipRequest(
    Guid UserId,
    List<Guid> AdditionalMembers,
    Guid MembershipOptionId,
    Guid MembershipLevelId);