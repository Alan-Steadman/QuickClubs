namespace QuickClubs.Domain.EventTypes.ValueObjects;

public sealed record EventTypeName(string Value)
{
    public const int MaxLength = 20;
}
