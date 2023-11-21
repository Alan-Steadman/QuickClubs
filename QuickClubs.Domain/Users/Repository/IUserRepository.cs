using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.Users.Repository;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    void Add(User user);
    void Update(User club);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
