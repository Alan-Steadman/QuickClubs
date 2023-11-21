using Microsoft.EntityFrameworkCore;
using QuickClubs.Domain.Users;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;
using System.Threading;

namespace QuickClubs.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {

        var userEmail = new UserEmail(email);
        return await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == userEmail, cancellationToken);
    }
}
