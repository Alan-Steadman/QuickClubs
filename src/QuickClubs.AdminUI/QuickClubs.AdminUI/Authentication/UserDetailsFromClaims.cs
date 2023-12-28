namespace QuickClubs.AdminUI.Authentication;

public record UserDetailsFromClaims(
    string UserId,
    string FirstName,
    string LastName,
    string Email,
    string Jti)
{
    public string FullName => $"{FirstName} {LastName}";
}