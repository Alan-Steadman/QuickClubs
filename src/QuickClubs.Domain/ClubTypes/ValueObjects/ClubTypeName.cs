namespace QuickClubs.Domain.ClubTypes.ValueObjects;
public sealed record ClubTypeName(string Value)
{
    public const int MaxLength = 25;
}
