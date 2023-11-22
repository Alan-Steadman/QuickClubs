namespace QuickClubs.Domain.MembershipOptions.ValueObjects;
public sealed record MembershipLevelName(string Value)
{
    public const int MaxLength = 12;
}
