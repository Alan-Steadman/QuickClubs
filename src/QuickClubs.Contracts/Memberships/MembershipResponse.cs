namespace QuickClubs.Contracts.Memberships;

public sealed record MembershipResponse(
    Guid Id,
    Guid ClubId,
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
    ApprovalResponse Approval,
    List<MembershipMemberResponse> Members);

public sealed record MembershipMemberResponse(
    Guid Id,
    Guid UserId,
    string FirstName,
    string LastName);