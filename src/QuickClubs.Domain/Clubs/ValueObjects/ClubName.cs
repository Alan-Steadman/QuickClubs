namespace QuickClubs.Domain.Clubs.ValueObjects;
public record ClubName(string FullName, string Acronym)
{
    public const int FullNameMaxLength = 50;
    public const int AcronymMaxLength = 8;
}