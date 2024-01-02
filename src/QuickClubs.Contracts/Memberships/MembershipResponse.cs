namespace QuickClubs.Contracts.Memberships;

public sealed record MembershipResponse(
    Guid MemberId,
    string MembershipNumber,
    string MembershipName,
    DateTime EndDate,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email);
