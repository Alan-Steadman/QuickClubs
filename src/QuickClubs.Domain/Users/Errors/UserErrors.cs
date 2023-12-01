using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.Users.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid id) => new("User.NotFound", $"The user with the id '{id}' was not found");
    public static Error InvalidCredentials => new("User.InvalidCredentials", "The provided credentials were invalid");
    public static Error DuplicateEmail = new("User.DuplicateEmail", "A user already exists with that email");

    public static Error ProfileExists = new("User.ProfileExists", "A profile already exists");
}
