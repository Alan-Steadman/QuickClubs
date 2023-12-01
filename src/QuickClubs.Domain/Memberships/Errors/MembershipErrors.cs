using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.Memberships.Errors;

public static class MembershipErrors
{
    public static Error NotFound(Guid id) => new ("Membership.NotFound", $"No membership was found with the id {id}");
    public static Error AlreadyApproved => new ("Membership.AlreadyApproved", "This membership is already approved");
}
