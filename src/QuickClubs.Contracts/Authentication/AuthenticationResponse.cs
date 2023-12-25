namespace QuickClubs.Contracts.Authentication;
public record AuthenticationResponse(
    string UserId,
    string FirstName,
    string LastName,
    string Email,
    string Token);