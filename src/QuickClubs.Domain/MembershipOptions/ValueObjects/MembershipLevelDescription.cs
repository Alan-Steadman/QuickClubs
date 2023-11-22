namespace QuickClubs.Domain.MembershipOptions.ValueObjects;

public sealed record MembershipLevelDescription(string Value)
{
    public const int MaxLength = 120;
}
