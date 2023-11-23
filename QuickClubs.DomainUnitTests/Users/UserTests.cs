using QuickClubs.Domain.Users.Events;
using QuickClubs.Domain.Users.ValueObjects;
using QuickClubs.Domain.Users;

namespace QuickClubs.DomainUnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void CreateUser_Should_Raise_UserCreatedDomainEvent()
    {
        // Arrange
        var firstName = new FirstName("first");
        var lastName = new LastName("last");
        var email = new UserEmail("test@test.com");
        var passwordHash = new PasswordHash("passwordHash");

        // Act
        var user = User.Create(firstName, lastName, email, passwordHash);

        // Assert
        var userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        userCreatedDomainEvent.UserId.Should().Be(user.Id);
    }
}
