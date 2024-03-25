namespace QuickClubs.Contracts.Memberships;

/// <summary>
/// A MemberResponse is used when you want to find all club members.  Hence it includes their contact details.
/// It is different to a MembershipResponse which is used when you want specific membership details, eg approval status, etc.
/// </summary>
public sealed record MemberResponse(
    Guid Id,
    string MembershipNumber,
    string MembershipName,
    DateTime EndDate,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email);
