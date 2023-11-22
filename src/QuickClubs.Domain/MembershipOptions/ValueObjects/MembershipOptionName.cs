namespace QuickClubs.Domain.MembershipOptions.ValueObjects;

public sealed record MembershipOptionName(string Value)
{
    public const int MaxLength = 25;
}
