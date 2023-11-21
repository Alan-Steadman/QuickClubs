namespace QuickClubs.Domain.Users.ValueObjects;

public record UserEmail(string Value)
{
    public const int MaxLength = 100;
}
