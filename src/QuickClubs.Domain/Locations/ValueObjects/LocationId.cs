namespace QuickClubs.Domain.Locations.ValueObjects;
public sealed record LocationId(Guid Value)
{
    public static LocationId New() =>
        new (Guid.NewGuid());
}
