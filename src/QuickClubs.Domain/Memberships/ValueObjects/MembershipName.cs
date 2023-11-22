namespace QuickClubs.Domain.Memberships.ValueObjects;

public sealed record MembershipName(string Value)
{
    public const int MaxLength = 40;
}
