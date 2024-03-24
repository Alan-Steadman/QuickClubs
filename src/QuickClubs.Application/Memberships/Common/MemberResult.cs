namespace QuickClubs.Application.Memberships.Common;

/// <summary>
/// A MemberResult is used when you want to find all club members.
/// Hence it includes their personal details.
/// It is different to a MembershipResult which is used when you want membership details, eg approval status/etc
/// </summary>
public sealed record MemberResult(
    Guid Id,
    string MembershipNumber,
    string MembershipName,
    DateTime EndDate,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email);