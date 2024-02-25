namespace QuickClubs.Domain.Locations.ValueObjects;

public sealed record OsGridRef(string Value)
{
    public const int MaxLength = 10; // eg "TX12341234" for an eight digit grid ref accurate to 10m.  The first two characters are I think the map sheet that the grid ref is on.
}
