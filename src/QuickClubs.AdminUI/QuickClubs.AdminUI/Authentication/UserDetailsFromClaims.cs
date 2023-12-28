namespace QuickClubs.AdminUI.Authentication;

public class UserDetailsFromClaims
{
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Jti { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";
}
