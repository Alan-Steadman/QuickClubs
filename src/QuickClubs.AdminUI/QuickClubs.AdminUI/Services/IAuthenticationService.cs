using QuickClubs.Contracts.Authentication;

namespace QuickClubs.AdminUI.Services;

public interface IAuthenticationService
{
    Task<bool> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
