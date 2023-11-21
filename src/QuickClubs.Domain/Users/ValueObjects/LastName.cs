namespace QuickClubs.Domain.Users.ValueObjects;

public record LastName(string Value)
{
    public const int MaxLength = 30;
    public const int MinLength = 2;
}