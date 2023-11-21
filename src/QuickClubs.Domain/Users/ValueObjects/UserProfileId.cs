namespace QuickClubs.Domain.Users.ValueObjects;

public record UserProfileId(Guid Value)
{
    public static UserProfileId New() => new(Guid.NewGuid());
}