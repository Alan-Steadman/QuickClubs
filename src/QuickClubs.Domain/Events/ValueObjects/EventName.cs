namespace QuickClubs.Domain.Events.ValueObjects;

public sealed record EventName(string Value)
{
    public const int MaxLengthAttribute = 30;

}
