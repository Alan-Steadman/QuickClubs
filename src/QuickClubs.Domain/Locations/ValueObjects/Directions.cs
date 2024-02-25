namespace QuickClubs.Domain.Locations.ValueObjects;

public sealed record Directions(string Value)
{
    public int MaxLength = 250;
}
