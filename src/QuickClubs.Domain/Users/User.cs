﻿using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Users.Entities;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Events;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private User(
        UserId id,
        FirstName firstName,
        LastName lastName,
        UserEmail email,
        PasswordHash passwordHash,
        UserProfile? profile)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Profile = profile;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public UserEmail Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public DateTime Registered { get; private set; }
    public DateTime LastLogin { get; private set; }
    public UserProfile? Profile { get; private set; }
    public bool HasProfile => Profile != null;

    public static User Create(
        FirstName firstName,
        LastName lastName,
        UserEmail email,
        PasswordHash passwordHash)
    {

        var user = new User(
            UserId.New(),
            firstName,
            lastName,
            email,
            passwordHash,
            profile: null);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public Result SetProfile(
        DateOnly dateOfBirth,
        Address address)
    {
        if (Profile is not null)
        {
            return Result.Failure(UserErrors.ProfileExists);
        }

        var profile = UserProfile.Create(
            this,
            dateOfBirth,
            address);

        Profile = profile;

        return Result.Success();
    }

    public void SetLastLoginNow(DateTime now)
    {
        this.LastLogin = now;
    }

    public void SetRegisteredNow(DateTime now)
    {
        this.Registered = now;
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
