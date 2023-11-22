namespace QuickClubs.Domain.MembershipOptions.ValueObjects;

public sealed record MembershipLevelId(Guid Value)
{
    public static MembershipLevelId New() => new(Guid.NewGuid());
}
