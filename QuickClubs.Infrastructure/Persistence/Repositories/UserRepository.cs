using Microsoft.EntityFrameworkCore;
using QuickClubs.Domain.Users;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsEmailUniqueAsync(UserId? id, string email, CancellationToken cancellationToken = default)
    {
        return !await DbContext
            .Set<User>()
            .AnyAsync(user => user.Email.Value == email, cancellationToken);
    }
}
