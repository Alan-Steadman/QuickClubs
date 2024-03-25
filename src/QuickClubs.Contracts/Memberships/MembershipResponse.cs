namespace QuickClubs.Contracts.Memberships;

public sealed record MembershipResponse(
    Guid Id,
    Guid ClubId,
    List<MembershipMemberResponse> Members,
    Guid MembershipOptionId,
    Guid MembershipLevelId,
    DateTime StartDate,
    DateTime EndDate,
    string MembershipNumber,
    string MembershipName,
    decimal PriceAmount,
    string PriceCurrency,
    string PriceFormatted,
    bool Paid,
    ApprovalResponse Approval);

public sealed record MembershipMemberResponse(
    Guid Id,
    Guid UserId,
    string FirstName,
    string LastName);