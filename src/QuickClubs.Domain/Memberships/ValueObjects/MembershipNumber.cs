namespace QuickClubs.Domain.Memberships.ValueObjects;

public sealed record MembershipNumber(string Value)
{
    public const int MaxLength = 20;
}
