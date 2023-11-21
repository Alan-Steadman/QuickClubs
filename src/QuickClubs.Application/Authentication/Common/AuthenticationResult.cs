namespace QuickClubs.Application.Authentication.Common;
public sealed record AuthenticationResult(
    string UserId,
    string FirstName,
    string LastName,
    string Email,
    string Token);