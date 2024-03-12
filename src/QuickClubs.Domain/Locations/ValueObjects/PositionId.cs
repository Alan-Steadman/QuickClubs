namespace QuickClubs.Domain.Locations.ValueObjects;

public sealed record PositionId(Guid Value)
{
    public static PositionId New() =>
        new(Guid.NewGuid());
}