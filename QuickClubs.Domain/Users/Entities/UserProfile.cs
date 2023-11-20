using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.Users.Entities;

public class UserProfile : Entity<UserProfileId>
{

    public User User { get; private set; }
    //public UserId UserId { get; private set; }

    public DateOnly DateOfBirth { get; private set; }
    public Address Address { get; private set; }

    public const int MinYearsOld = 13;
    public const int MaxYearsOld = 120;

    private UserProfile(
        UserProfileId id,
        //UserId userId,
        User user,
        DateOnly dateOfBirth,
        Address address)
        : base(id)
    {
        User = user;
        //UserId = userId;
        DateOfBirth = dateOfBirth;
        Address = address;
    }

    public static UserProfile Create(
        //UserId userId,
        User user,
        DateOnly dateOfBirth,
        Address address)
    {
        return new UserProfile(
            UserProfileId.New(),
            //userId,
            user,
            dateOfBirth,
            address);
    }

#pragma warning disable CS8618
    private UserProfile()
    {
    }
#pragma warning restore CS8618
}
