namespace QuickClubs.Domain.Memberships.ValueObjects;

public sealed record MembershipId(Guid Value)
{
    public static MembershipId New() => new(Guid.NewGuid());
}
