namespace QuickClubs.Application.Memberships.Common;

public sealed record MemberResult(
    Guid MemberId,
    string MembershipNumber,
    string MembershipName,
    DateTime EndDate,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email);