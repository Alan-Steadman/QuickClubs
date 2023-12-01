using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.MembershipOptions.Errors;
public static class MembershipOptionErrors
{
    public static Error NotFound(Guid id) => new Error("MembershipOption.NotFound", $"No membership option was found with the id '{id}'");

    public static Error MembershipLevelNotFound(Guid id) => new Error("MembershipOption.MembershipLevelNotFound", $"No membership level was found with the id '{id}'");
}
