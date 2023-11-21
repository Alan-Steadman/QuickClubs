namespace QuickClubs.Domain.Clubs.ValueObjects;

public record ClubWebsite(string Url)
{
    public const int MaxLength = 80;
}
