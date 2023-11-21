using QuickClubs.Domain.Users;

namespace QuickClubs.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
