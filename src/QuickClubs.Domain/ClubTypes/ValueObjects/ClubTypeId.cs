namespace QuickClubs.Domain.ClubTypes.ValueObjects;
public sealed record ClubTypeId(Guid Value)
{
    public static ClubTypeId New() => new(Guid.NewGuid());
}
