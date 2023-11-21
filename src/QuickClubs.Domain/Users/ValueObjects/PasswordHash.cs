namespace QuickClubs.Domain.Users.ValueObjects;

public sealed record PasswordHash(string Value)
{
    public const int MaxLength = 75;

    public const int PasswordMinLength = 8; // TODO: Decide actual minimum password length
    public const string PasswordRegex = ""; // TODO: Create an actual regex that represents strong password requirements
    public const string PasswordRegexMessage = "The password must have at least ...."; // TODO create a message that describes the password requirements
}
