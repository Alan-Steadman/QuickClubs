using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.Memberships.Errors;

public static class MembershipErrors
{
    public static Error NotFound(Guid id) => new ("Membership.NotFound", $"No membership was found with the id {id}");
    public static Error AlreadyApproved => new ("Membership.AlreadyApproved", "This membership is already approved");
    public static Error NotInARejectableState => new("Membership.NotInARejectableState", "This membership is not in a state that can be rejected");
}
