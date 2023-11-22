namespace QuickClubs.Domain.MembershipOptions.ValueObjects;
public sealed record MembershipOptionId(Guid Value)
{
    public static MembershipOptionId New() => new(Guid.NewGuid());
}
