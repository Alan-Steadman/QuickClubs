namespace QuickClubs.Domain.Locations.ValueObjects;

public sealed record Directions(string Value)
{
    public const int MaxLength = 250;
}
