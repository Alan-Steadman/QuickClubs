namespace QuickClubs.Domain.Clubs.ValueObjects;

public record ClubSettingsId(Guid Value)
{
    public static ClubSettingsId New() => new(Guid.NewGuid());
}
