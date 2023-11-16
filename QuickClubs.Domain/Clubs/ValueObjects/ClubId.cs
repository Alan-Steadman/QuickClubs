namespace QuickClubs.Domain.Clubs.ValueObjects;

public record ClubId(Guid Value)
{
    public static ClubId New() => new(Guid.NewGuid());
}
