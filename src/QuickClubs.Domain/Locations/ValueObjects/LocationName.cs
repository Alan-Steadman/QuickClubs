namespace QuickClubs.Domain.Locations.ValueObjects;
public sealed record LocationName(string Value)
{
    public const int MaxLength = 40;
}
