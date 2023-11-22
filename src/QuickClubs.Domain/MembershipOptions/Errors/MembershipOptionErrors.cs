using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.MembershipOptions.Errors;
public static class MembershipOptionErrors
{
    public static Error NotFound => new Error("MembershipOption.NotFound", "No membership option was found with that identifier");

    public static Error MembershipLevelNotFound => new Error("MembershipOption.MembershipLevelNotFound", "No membership level was found with that identifier");
}
